namespace SocketSemServer
{
    public enum GameCommand : byte
    {
        None = 0x00,
        Attack = 0x01,
        Defense = 0x02,
        ActionResult = 0x03,
        HealthUpdate = 0x04,
        EndGame = 0x05
    }

}
