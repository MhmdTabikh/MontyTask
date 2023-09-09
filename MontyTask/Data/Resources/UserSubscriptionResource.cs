using MontyTask.Data.Models;

namespace MontyTask.Data.Resources
{
    public class UserSubscriptionResource
    {
        public string UserEmail { get; set; } = string.Empty;
        public SubscriptionType SubscriptionType { get; set; }
    }
}
