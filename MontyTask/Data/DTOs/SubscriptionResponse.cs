using MontyTask.Data.Models;

namespace MontyTask.Data.DTOs;

public class SubscriptionResponse : BaseResponse
{
    public List<Subscription>? Subscriptions { get; private set; } 

    public SubscriptionResponse(bool success, string? message, List<Subscription>? subscriptions) : base(success, message)
    {
        Subscriptions = subscriptions;
    }
}
