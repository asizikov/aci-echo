using System;

namespace Echo.Api;

public class AppNameProvider : IAppNameProvider
{
    public string GetAppName()
    {
        return Environment.GetEnvironmentVariable(Constants.APP_NAME);
    }
}