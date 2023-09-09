namespace MontyTask.Data.Resources;
public class AccessTokenResource
{
    public string AccessToken { get; set; } = string.Empty;
    public long Expiration { get; set; }
}