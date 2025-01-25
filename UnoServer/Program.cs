using System.Net;
using UnoServer;


var server = new Server(new IPAddress(new byte[] { 127, 0, 0, 1 }), 12345);

await server.StartAsync();

Console.WriteLine("Server was stoped!");
