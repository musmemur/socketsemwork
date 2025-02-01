using System.Net.Sockets;

namespace SocketSemServer
{
    public class Player(Socket socket)
    {
        public Socket Socket { get; private set; } = socket;
        public int Health { get; set; } = 1;
        public string[] Actions { get; set; } = new string[3];
        public bool WantsNewGame { get; set; } = false;
    }
}
