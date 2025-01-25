using System.Net.Sockets;

namespace UnoServer
{
    public class Player
    {
        public Socket Socket { get; private set; }
        public int Health { get; set; } = 10;
        public string[] Actions { get; set; } = new string[3];

        public Player(Socket socket)
        {
            Socket = socket;
        }
    }
}
