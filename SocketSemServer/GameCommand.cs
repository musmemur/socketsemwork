namespace SocketSemServer
{
    public enum GameCommand : byte
    {
        Attack = 0x01,
        Defense = 0x02,
        ActionResult = 0x03,
        HealthUpdate = 0x04,
    }
}
