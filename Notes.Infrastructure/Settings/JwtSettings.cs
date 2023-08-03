namespace Notes.Infrastructure.Settings;

public class JwtSettings
{
    public string SecretKey { get; init; } = null!;

    public string Issuer { get; init; } = null!;
    
    public string Audience { get; init; } = null!;
    
    public int ExpirationHours { get; init; } 
}