using System.Net;

namespace BuildingBlocks.Exceptions;

public class ConfigurationNotFoundException : CustomException
{
    public ConfigurationNotFoundException(string settingName) : base(string.Format("Configurations for {0} Not Found.", settingName), null, HttpStatusCode.InternalServerError)
    {
    }
}
