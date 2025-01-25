using GSF.Communication;
using System.Net;
using System.Windows;
using UnoServer;
using UnoWpf.Views;

namespace UnoWpf
{
    public partial class MainWindow : Window
    {
        private Server _server;
        private Thread _serverThread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartServer_Click(object sender, RoutedEventArgs e)
        {
            string ip = IpTextBox.Text;
            int port;

            if (!int.TryParse(PortTextBox.Text, out port))
            {
                MessageBox.Show("Пожалуйста, введите правильный порт.");
                return;
            }

            _serverThread = new Thread(() => StartServer(ip, port));
            _serverThread.Start();
        }

        private void StartServer(string ip, int port)
        {
            try
            {
                _server = new Server(new IPAddress(new byte[] { 127, 0, 0, 1 }), 12345);
                _server.StartAsync();

                Dispatcher.Invoke(() => LogListBox.Items.Add("Сервер запущен..."));
            }
            catch (Exception ex)
            {
                Dispatcher.Invoke(() => LogListBox.Items.Add($"Ошибка при запуске сервера: {ex.Message}"));
            }
        }

        private void OpenClientWindow_Click(object sender, RoutedEventArgs e)
        {
            var clientWindow = new ClientWindow();
            clientWindow.Show();
        }
    }
}
