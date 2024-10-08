namespace PWPA.CallLogging.BackEnd.Infrastructure;

public class CallsDatabaseSettings
{
    public string? ConnectionString { get; set; }

    public string? Database { get; set; }

    public string? CallsCollectionName { get; set; }
}