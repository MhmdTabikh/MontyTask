using MontyTask.Data;
using MontyTask.Data.DTOs;
using MontyTask.Data.Models;
using MontyTask.Data.Repositories;

namespace MontyTask.Services;

//if the services are to be scalable,interface would then be in an independent file
public interface ISubscriptionService
{
    Task<SubscriptionResponse> GetSubscriptionsByEmailAsync(string email);
}
public class SubscriptionService : ISubscriptionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISubscriptionRepository _subscriptionRepository;

    public SubscriptionService(ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _subscriptionRepository = subscriptionRepository;
    }


    public async Task<SubscriptionResponse> GetSubscriptionsByEmailAsync(string email)
    {
        var subscriptions =  await _subscriptionRepository.FindByEmailAsync(email);
        if(subscriptions is null)
        {
            return new SubscriptionResponse(true, $"No subscriptions were found for user {email}", Enumerable.Empty<Subscription>());
        }

        return new SubscriptionResponse(true, null, subscriptions);
    }
}
