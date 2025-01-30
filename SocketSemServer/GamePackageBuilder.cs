namespace SocketSemServer
{
    public class GamePackageBuilder
    {
        private readonly byte[] _package;

        public GamePackageBuilder(int contentSize)
        {
            if (contentSize > GamePackageHelper.MaxContentSize)
            {
                throw new ArgumentException($"Content size must not exceed {GamePackageHelper.MaxContentSize}");
            }
            _package = new byte[GamePackageHelper.BaseHeaderSize + contentSize];
            CreateBasePackage();
        }

        private void CreateBasePackage()
        {
            Array.Copy(GamePackageHelper.BaseHeader, _package, GamePackageHelper.BaseHeader.Length);
            _package[^1] = GamePackageHelper.EndByte;
        }

        public GamePackageBuilder WithCommand(GameCommand command)
        {
            _package[GamePackageHelper.CommandIndex] = (byte)command;
            return this;
        }

        public GamePackageBuilder WithFullness(FullnessPackage fullness)
        {
            _package[GamePackageHelper.FullnessIndex] = (byte)fullness;
            return this;
        }

        public GamePackageBuilder WithQueryType(QueryType queryType)
        {
            _package[GamePackageHelper.QueryIndex] = (byte)queryType;
            return this;
        }

        public GamePackageBuilder WithContent(byte[] content)
        {
            if (content.Length > _package.Length - GamePackageHelper.BaseHeaderSize)
            {
                throw new ArgumentException("Content size exceeds package capacity.");
            }
            Array.Copy(content, 0, _package, GamePackageHelper.BaseHeaderSize, content.Length);
            return this;
        }

        public byte[] Build() => _package;
    }
}
