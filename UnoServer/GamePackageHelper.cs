namespace UnoServer
{
    public static class GamePackageHelper
    {
        public const int MaxContentSize = 244;
        public const int BaseHeaderSize = 12;
        public static readonly byte[] BaseHeader = new byte[BaseHeaderSize];
        public const byte EndByte = 0xFF;

        public const int CommandIndex = 0;
        public const int FullnessIndex = 1;
        public const int QueryIndex = 2;
    }
}
