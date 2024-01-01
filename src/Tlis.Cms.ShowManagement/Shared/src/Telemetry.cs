using System.Diagnostics;

namespace Tlis.Cms.ShowManagement.Shared;

public static class Telemetry
{
    public static readonly string ServiceName = "";

    public static readonly ActivitySource ActivitySource = new(ServiceName);
}