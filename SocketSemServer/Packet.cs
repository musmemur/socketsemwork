using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketSemServer
{
    public class Packet
    {
        private const byte Header1 = 0xAF;
        private const byte Header2 = 0xAA;
        private const byte Header3 = 0xAF;
        private const byte EndMarker1 = 0xEF;
        private const byte EndMarker2 = 0xBE;

        public required string Command { get; set; }
        public required byte[] Data { get; set; }

        public byte[] ToBytes()
        {
            var commandBytes = Encoding.UTF8.GetBytes(Command);
            var lengthCommand = (byte)(commandBytes.Length);
            var lengthData = (byte)(Data?.Length ?? 0);

            var packet = new byte[3 + 1 + 1 + commandBytes.Length + (Data?.Length ?? 0) + 2];
            packet[0] = Header1;
            packet[1] = Header2;
            packet[2] = Header3;

            packet[3] = lengthCommand;
            packet[4] = lengthData;

            Array.Copy(commandBytes, 0, packet, 5, commandBytes.Length);

            if (Data != null && Data.Length > 0)
            {
                Array.Copy(Data, 0, packet, 5 + commandBytes.Length, Data.Length);
            }

            packet[packet.Length - 2] = EndMarker1;
            packet[packet.Length - 1] = EndMarker2;

            Console.WriteLine("Создан пакет:");
            Console.WriteLine($"  Заголовки: {Header1}, {Header2}, {Header3}");
            Console.WriteLine($"  Длина: {lengthCommand}");
            Console.WriteLine($"  Длина: {lengthData}");
            Console.WriteLine($"  Команда: '{Command}'");

            Console.WriteLine($"  Завершающие маркеры: {EndMarker1}, {EndMarker2}");

            return packet;
        }
        
        public static Packet FromBytes(byte[] data)
        {
            try
            {

                var length = data[3];
                var commandBytes = new byte[length];
                Array.Copy(data, 5, commandBytes, 0, length);

                var packet = new Packet
                {
                    Command = Encoding.UTF8.GetString(commandBytes),
                    Data = data.Skip(5 + commandBytes.Length).Take(data.Length - (7 + commandBytes.Length)).ToArray()
                };

                Console.WriteLine($"Успешно соdfghздан пакет. Команда: '{packet.Command}', Длина данных: {packet.Data.Length}");

                return packet;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибк2345676543 данных: {ex.Message}");
                throw new InvalidOperationException($"чтс пакет {ex.Message}");
            }
        } 
    }
}
