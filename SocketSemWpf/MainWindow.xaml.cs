﻿using System.Net;
using System.Windows;
using SocketSemServer;
using SocketSemWpf.Views;

namespace SocketSemWpf
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

            if (!int.TryParse(PortTextBox.Text, out port) || port <= 0 || port > 65535)
            {
                MessageBox.Show("Пожалуйста, введите правильный порт (1–65535).");
                return;
            }

            _serverThread = new Thread(() => StartServer(ip, port));
            _serverThread.Start();
        }

        private void StartServer(string ip, int port)
        {
            try
            {
                var ipAddress = IPAddress.Parse(ip);
                _server = new Server(ipAddress, port);
                _server.StartAsync();

                Dispatcher.Invoke(() => LogListBox.Items.Add($"Сервер запущен на {ip}:{port}..."));
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
