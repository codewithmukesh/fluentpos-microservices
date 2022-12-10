using System.Net;

namespace BuildingBlocks.Exceptions;

public class ConfigurationNotFoundException : CustomException
{
    public ConfigurationNotFoundException(string settingName) : base(string.Format("Configuration for {0} Not Found.", settingName), null, HttpStatusCode.InternalServerError)
    {
    }
}
