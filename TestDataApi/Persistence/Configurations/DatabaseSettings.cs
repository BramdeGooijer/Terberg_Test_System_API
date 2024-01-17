namespace TestDataApi.Persistence.Configurations;

public class DatabaseSettings
{
    public const string SectionName = "DatabaseSettings";
    
    public string Host { get; init; } = null!;
    public string Username { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Database { get; init; } = null!;}