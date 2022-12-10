namespace BuildingBlocks.Options;

public class EventBusSettings
{
    public RMQ? RMQ { get; set; }
}

public class RMQ
{
    public bool Enable { get; set; }
    public string? Host { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int Port { get; set; }

}