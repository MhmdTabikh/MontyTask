using MontyTask.Data.Models;

namespace MontyTask.Data.Resources;

public class SubscriptionResource
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SubscriptionType SubscriptionType { get; set; } 

}
