using MontyTask.Data.Models;

namespace MontyTask.Data.DTOs;

public class SubscriptionResponse : BaseResponse
{
    public IEnumerable<Subscription> Subscriptions { get; private set; }

    public SubscriptionResponse(bool success, string? message, IEnumerable<Subscription> subscriptions) : base(success, message)
    {
        Subscriptions = subscriptions;
    }
}
