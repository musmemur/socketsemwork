using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UnoWpf.Views
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private int _currentActions = 0;
        private readonly string[] _selectedActions = new string[3];

        public ClientWindow()
        {
            InitializeComponent();
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

                Thread receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
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

                    Dispatcher.Invoke(() =>
                    {
                        MessageListBox.Items.Add($"Сервер: {message}");

                        if (message.Contains("Начинаем выбор действий"))
                        {
                            ResetActions();
                            ActionPanel.Visibility = Visibility.Visible;
                        }

                        if (message.Contains("Раунд завершён"))
                        {
                            ResetActions();
                        }

                        // Если игра завершена, скрываем панель действий
                        if (message.Contains("Игра окончена"))
                        {
                            ActionPanel.Visibility = Visibility.Collapsed;
                            MessageBox.Show(message, "Конец игры");
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



        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentActions >= 3)
            {
                MessageBox.Show("Вы уже выбрали 3 действия!");
                return;
            }

            if (sender is Button button && button.Tag is string action)
            {
                _selectedActions[_currentActions] = action; // Сохраняем выбранное действие
                _currentActions++;
                MessageListBox.Items.Add($"Выбрано: {action}");

                // Блокируем кнопки текущего хода
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

                // Если выбрано 3 действия, отправляем их на сервер
                if (_currentActions == 3)
                {
                    SendActionsToServer();
                }
            }
        }

        private void ResetActions()
        {
            _currentActions = 0; // Сбрасываем счётчик действий
            Array.Clear(_selectedActions, 0, _selectedActions.Length); // Очищаем массив действий

            // Включаем все кнопки в ActionPanel
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

        private StackPanel FindParentStackPanel(DependencyObject child)
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
}
