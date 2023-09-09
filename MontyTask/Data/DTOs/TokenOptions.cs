namespace MontyTask.Data.DTOs;

public class TokenOptions
{
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public long AccessTokenExpiration { get; set; }
    public long RefreshTokenExpiration { get; set; }
    public string Secret { get; set; } = string.Empty;
}