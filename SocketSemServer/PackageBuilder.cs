using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketSemServer
{
    using static PackageHelper;

    public enum FullnessPackage
    {
        Full = 0x46,
        Partial = 0x50
    }

    public enum QueryType : byte
    {
        Request = 0x3F,
        Response = 0x21
    }
    public enum GameCommand : byte
    {
        Attack = 0x01,
        Defense = 0x02,
        ActionResult = 0x03,
        HealthUpdate = 0x04,
        ActionRequest = 0x05,
        ActionError = 0x06,
    }

    public class PackageBuilder
    {
        private readonly byte[] _package;

        public PackageBuilder(int sizeOfContent)
        {
            if (sizeOfContent > MaxSizeOfContent)
            {
                throw new ArgumentException(
                    $"size of content must be less or equal {nameof(MaxSizeOfContent)}",
                    nameof(sizeOfContent));
            }

            _package = new byte[MaxFreeBytes + sizeOfContent];
            CreateBasePackage();
        }

        private void CreateBasePackage()
        {
            Array.Copy(BasePackage, _package, BasePackage.Length);

            _package[^1] = LastByte;
        }

        public PackageBuilder WithCommand(GameCommand command)
        {
            _package[Command] = (byte)command;

            return this;
        }

        public PackageBuilder WithFullness(FullnessPackage fullness)
        {
            _package[Fullness] = (byte)fullness;

            return this;
        }

        public PackageBuilder WithQueryType(QueryType queryType)
        {
            _package[Query] = (byte)queryType;

            return this;
        }

        public PackageBuilder WithContent(byte[] content)
        {
            if (content.Length > _package.Length - MaxFreeBytes)
            {
                throw new ArgumentException(nameof(content));
            }

            for (var i = 0; i < content.Length; i++)
            {
                _package[i + MaxFreeBytes - 1] = content[i];
            }

            return this;
        }

        public byte[] Build()
        {
            return _package;
        }
    }
}
