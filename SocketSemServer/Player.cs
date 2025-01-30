using System.Net.Sockets;

namespace SocketSemServer
{
    public class Player
    {
        public Socket Socket { get; private set; }
        public int Health { get; set; } = 10;
        public string[] Actions { get; set; } = new string[3];
        
        public bool WantsNewGame { get; set; } = false;

        public Player(Socket socket)
        {
            Socket = socket;
        }
    }
}
