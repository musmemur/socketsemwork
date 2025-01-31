using System.Net;
using SocketSemServer;


var server = new Server(new IPAddress([127, 0, 0, 1]), 12345);

await server.StartAsync();

Console.WriteLine("Server was stoped!");
