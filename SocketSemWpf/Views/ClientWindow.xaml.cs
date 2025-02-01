using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SocketSemWpf.Views;

public partial class ClientWindow : Window
{
    private TcpClient _client;
    private NetworkStream _stream;
    private int _currentActions = 0;
    private readonly string[] _selectedActions = new string[3];

    public ClientWindow()
    {
        InitializeComponent();
        NewGameButton.Visibility = Visibility.Collapsed; 
    }

    private void Connect_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string ip = IpTextBox.Text;
            int port = int.Parse(PortTextBox.Text);

            _client = new TcpClient(ip, port);
            _stream = _client.GetStream();
            MessageListBox.Items.Add("Подключено к серверу!");

            var receiveThread = new Thread(ReceiveMessages)
            {
                IsBackground = true
            };
            receiveThread.Start();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка подключения: {ex.Message}");
        }
    }

    private void ReceiveMessages()
    {
        try
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                message = message.Replace("\0", "").Replace("\u0004", "").Trim();
                message = message.Replace("\r\n", "\n");

                Dispatcher.Invoke(() =>
                {
                    MessageListBox.Items.Add($"{message}");

                    if (message.Contains("Начинаем выбор действий"))
                    {
                        ConnectButton.Visibility = Visibility.Collapsed;
                        ResetActions();
                        ActionPanel.Visibility = Visibility.Visible;
                    }

                    if (message.Contains("Раунд завершён"))
                    {
                        ResetActions();
                    }

                    if (message.Contains("Игра окончена"))
                    {
                        ActionPanel.Visibility = Visibility.Collapsed;
                        NewGameButton.Visibility = Visibility.Visible;

                        string winner = ExtractWinner(message);
                        string modifiedWinner = winner.Length == 5 ? winner : winner[..8];

                        MessageBox.Show($"Игра окончена! Победитель: {modifiedWinner}", "Конец игры");
                    }

                    if (message.Contains("Оба игрока согласны на новую игру!"))
                    {
                        MessageBox.Show("Новая игра начинается!");
                        MessageListBox.Items.Clear();
                        ActionPanel.Visibility = Visibility.Visible;
                        ResetActions();
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show($"Ошибка приема сообщений: {ex.Message}");
            });
        }
    }
    private static string ExtractWinner(string message)
    {
        var lines = message.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        const string winnerPrefix = "Игра окончена! Победитель: ";

        foreach (var line in lines)
        {
            if (line.Contains(winnerPrefix))
            {
                int startIndex = line.IndexOf(winnerPrefix) + winnerPrefix.Length;
                string winnerPart = line[startIndex..].Trim();

                if (winnerPart.StartsWith("Игрок "))
                {
                    return winnerPart.Split('\n')[0];
                }
                else
                {
                    return "Ничья";
                }
            }
        }

        return "Неизвестно";
    }


    private void ActionButton_Click(object sender, RoutedEventArgs e)
    {
        if (_currentActions >= 3)
        {
            MessageBox.Show("Вы уже выбрали 3 действия!");
            return;
        }

        if (sender is Button button && button.Tag is string action)
        {
            _selectedActions[_currentActions] = action; 
            _currentActions++;
            MessageListBox.Items.Add($"Выбрано: {action}");

            var parentStackPanel = FindParentStackPanel(button);
            if (parentStackPanel != null)
            {
                foreach (var child in parentStackPanel.Children)
                {
                    if (child is Grid grid)
                    {
                        foreach (var gridChild in grid.Children)
                        {
                            if (gridChild is Button gridButton)
                            {
                                gridButton.IsEnabled = false; 
                            }
                        }
                    }
                }
            }

            if (_currentActions == 3)
            {
                SendActionsToServer();
            }
        }
    }

    private void ResetActions()
    {
        _currentActions = 0;
        Array.Clear(_selectedActions, 0, _selectedActions.Length);
            
        foreach (var child in ActionPanel.Children)
        {
            if (child is StackPanel stackPanel)
            {
                foreach (var stackPanelChild in stackPanel.Children)
                {
                    if (stackPanelChild is Grid grid)
                    {
                        foreach (var gridChild in grid.Children)
                        {
                            if (gridChild is Button button)
                            {
                                button.IsEnabled = true; 
                            }
                        }
                    }
                }
            }
        }
    }

    private void SendActionsToServer()
    {
        try
        {
            string actions = string.Join(";", _selectedActions);
            byte[] data = Encoding.UTF8.GetBytes(actions);
            _stream.Write(data, 0, data.Length);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка отправки действий: {ex.Message}");
        }
    }

    private void NewGameButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes("newgame");
            _stream.Write(data, 0, data.Length);
            MessageListBox.Items.Add("Запрос на новую игру отправлен серверу.");
            NewGameButton.Visibility = Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка отправки запроса на новую игру: {ex.Message}");
        }
    }

    private static StackPanel? FindParentStackPanel(DependencyObject child)
    {
        while (child != null)
        {
            if (child is StackPanel stackPanel)
            {
                return stackPanel;
            }

            child = VisualTreeHelper.GetParent(child);
        }

        return null;
    }
}