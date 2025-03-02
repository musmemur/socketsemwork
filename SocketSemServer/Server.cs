﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketSemServer
{
    public class Server
    {
        private readonly Socket _serverSocket;
        private readonly List<Player> _players = [];
        private const int MaxPlayers = 2;
        private const int MaxTimeout = 5 * 60 * 1000;

        public Server(IPAddress ipAddress, int port)
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(ipAddress, port));
        }

        public async Task StartAsync()
        {
            try
            {
                _serverSocket.Listen(10);
                Console.WriteLine("Сервер запущен. Ожидание игроков...");

                while (_players.Count < MaxPlayers)
                {
                    var cancellationTokenSource = new CancellationTokenSource();
                    cancellationTokenSource.CancelAfter(MaxTimeout);
                    cancellationTokenSource.Token.Register(StopAsync);
                    var clientSocket = await _serverSocket.AcceptAsync();

                    if (_players.Count >= MaxPlayers)
                    {
                        await SendMessageAsync(clientSocket, "Сессия заполнена, попробуйте позже.", GameCommand.ActionResult, QueryType.Response);
                        clientSocket.Close();
                        continue;
                    }

                    var player = new Player(clientSocket);
                    _players.Add(player);
                    Console.WriteLine($"Игрок {_players.Count} подключился.");
                    await SendMessageAsync(player.Socket, $"Добро пожаловать, Игрок {_players.Count}! ", GameCommand.ActionResult, QueryType.Response);
                }

                if (_players.Count == MaxPlayers)
                {
                    await BroadcastAsync("Все игроки подключились. Начинаем выбор действий.");
                    await StartGameAsync();
                }

                await HandleNewGameRequestsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        private async Task StartGameAsync()
        {
            await BroadcastAsync("Игра начинается! У каждого игрока по 10 жизней.");

            while (_players[0].Health > 0 && _players[1].Health > 0)
            {
                var tasks = _players.Select(player => ReceivePlayerActionsAsync(player)).ToArray();
                await Task.WhenAll(tasks);

                await ResolveActionsAsync();
                await BroadcastHealthAsync();
            }

            string winner;
            if (_players[0].Health <= 0 && _players[1].Health <= 0)
            {
                winner = "Ничья";
            }
            else
            {
                winner = _players[0].Health > 0 ? "Игрок 1" : "Игрок 2";
            }
            await BroadcastAsync($"Игра окончена! Победитель: {winner}. ");
            await BroadcastAsync("Если оба игрока хотят начать новую игру, нажмите кнопку 'Начать новую игру'.");
        }

        private async Task ReceivePlayerActionsAsync(Player player)
        {
            var playerIndex = _players.IndexOf(player) + 1;

            await SendMessageAsync(player.Socket, "Введите 3 действия", GameCommand.ActionRequest, QueryType.Request);

            string actions = await ReceiveMessageAsync(player);

            var actionsArray = actions.Split(';', StringSplitOptions.RemoveEmptyEntries);

            if (actionsArray.Length == 3 && actionsArray.All(ValidateAction))
            {
                player.Actions = actionsArray;

                await SendMessageAsync(player.Socket, "Ваши действия получены.", GameCommand.ActionResult, QueryType.Response);
            }
            else
            {
                await SendMessageAsync(player.Socket, "Некорректный формат действий. Попробуйте снова.", GameCommand.ActionError, QueryType.Response);
                await ReceivePlayerActionsAsync(player);
            }
        }

        private async Task ResolveActionsAsync()
        {
            for (int i = 0; i < 3; i++)
            {
                var action1 = _players[0].Actions[i].Split(' ');
                var action2 = _players[1].Actions[i].Split(' ');

                var action1Type = action1[0];
                var action1Target = action1[1];

                var action2Type = action2[0];
                var action2Target = action2[1];

                Console.WriteLine($"Ход {i + 1}:");
                Console.WriteLine($"Игрок 1: {action1Type} {action1Target}");
                Console.WriteLine($"Игрок 2: {action2Type} {action2Target}");

                bool player1Hit = action1Type == "attack" && (action2Type != "defense" || action1Target != action2Target);
                bool player2Hit = action2Type == "attack" && (action1Type != "defense" || action2Target != action1Target);

                if (player1Hit)
                {
                    _players[1].Health--;
                }

                if (player2Hit)
                {
                    _players[0].Health--;
                }
            }

            await BroadcastPackageAsync(GameCommand.ActionResult, "Раунд завершён. Начинаем следующий раунд. ");
        }


        private async Task BroadcastHealthAsync()
        {
            foreach (var player in _players)
            {
                var healthMessage = $"Ваши жизни: {player.Health}.{Environment.NewLine}";
                Console.WriteLine($"Игрок {(_players.IndexOf(player) + 1)}: {healthMessage}");

                var content = Encoding.UTF8.GetBytes(healthMessage);
                var package = PackageHelper.CreatePackage(content, GameCommand.HealthUpdate, FullnessPackage.Full, QueryType.Response);

                await SendPackageAsync(player.Socket, package[11..]);
            }
        }


        private async Task HandleNewGameRequestsAsync()
        {
            while (true)
            {
                foreach (var player in _players)
                {
                    var message = await ReceiveMessageAsync(player);
                    if (message == "newgame")
                    {
                        player.WantsNewGame = true;
                        await SendMessageAsync(player.Socket, "Вы запросили новую игру. Ожидаем второго игрока...");
                    }
                }

                if (_players.All(p => p.WantsNewGame))
                {
                    await BroadcastAsync("Оба игрока согласны на новую игру!");
                    ResetGame();
                    await StartGameAsync();
                }
            }
        }

        private void ResetGame()
        {
            foreach (var player in _players)
            {
                player.Health = 10;
                player.Actions = new string[3];
                player.WantsNewGame = false;
            }
            Console.WriteLine("Игра сброшена. Готово к новой игре.");
        }

        private bool ValidateAction(string action)
        {
            if (string.IsNullOrWhiteSpace(action)) return false;
            var parts = action.Split(' ');
            if (parts.Length != 2) return false;
            if (parts[0] != "attack" && parts[0] != "defense") return false;
            if (parts[1] != "top" && parts[1] != "middle" && parts[1] != "bottom") return false;
            return true;
        }

        private static async Task<string> ReceiveMessageAsync(Player player)
        {
            var buffer = new byte[512];
            var bytesRead = await player.Socket.ReceiveAsync(buffer, SocketFlags.None);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
        }

        private static async Task SendMessageAsync(Socket socket, string message, GameCommand command, QueryType queryType)
        {
            var content = Encoding.UTF8.GetBytes(message);
            var packages = PackageHelper.GetPackagesByMessage(content, command, queryType);

            foreach (var package in packages)
            {
                await socket.SendAsync(package[11..], SocketFlags.None);
            }
        }

        private static async Task SendMessageAsync(Socket socket, string message)
        {
            var data = Encoding.UTF8.GetBytes(message + Environment.NewLine);
            await socket.SendAsync(data, SocketFlags.None);
        }

        private async Task BroadcastAsync(string message)
        {
            foreach (var player in _players)
            {
                await SendMessageAsync(player.Socket, message, GameCommand.ActionResult, QueryType.Response);
            }
        }

        private static async Task SendPackageAsync(Socket socket, byte[] package)
        {
            await socket.SendAsync(package, SocketFlags.None);
        }

        private void StopAsync()
        {
            _serverSocket.Close();
        }

        private async Task BroadcastPackageAsync(GameCommand command, string message)
        {
            var content = Encoding.UTF8.GetBytes(message);
            var package = new PackageBuilder(content.Length)
                .WithCommand(command)
                .WithContent(content)
                .Build();

            foreach (var player in _players)
            {
                await SendPackageAsync(player.Socket, package[11..]);
            }
        }

    }
}
