namespace BuildingBlocks.Configs;

public class LoggerConfig
{
    public string? Level { get; set; } = "Information";
    public string? LogTemplate { get; set; } = "{Timestamp:HH:mm:ss} [{Level:u4}] {Message:lj}{NewLine}{Exception}";
    public bool PrintToConsole { get; set; } = true;
    public StructuredLoggerOptions StructuredLoggerOptions { get; set; } = new StructuredLoggerOptions();
    public ElasticSearchConfig ElasticSearchConfig { get; set; } = new ElasticSearchConfig();
}
public class StructuredLoggerOptions
{
    public bool Enable { get; set; } = true;
    public string RollingInterval { get; set; } = "Day";
    public string Path { get; set; } = "logs/logs.json";
}

public class ElasticSearchConfig
{
    public bool Enable { get; set; } = true;
    public string Uri { get; set; } = "http://localhost:9200";
}
