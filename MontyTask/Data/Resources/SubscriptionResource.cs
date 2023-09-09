using MontyTask.Data.Models;

namespace MontyTask.Data.Resources;

public class SubscriptionResource
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string SubscriptionType { get; set; } = string.Empty;

}
