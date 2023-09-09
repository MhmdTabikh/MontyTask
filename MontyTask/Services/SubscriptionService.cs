using MontyTask.Data;
using MontyTask.Data.DTOs;
using MontyTask.Data.Models;
using MontyTask.Data.Repositories;

namespace MontyTask.Services;

//if the services are to be scalable,interface would then be in an independent file
public interface ISubscriptionService
{
    Task<SubscriptionResponse> GetSubscriptionsByEmailAsync(string email);
    Task AddSubscription(string userEmail, SubscriptionType subscriptionType,DateTime startDate,DateTime endDate);
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

    public async Task AddSubscription(string userEmail, SubscriptionType subscriptionType,DateTime startDate,DateTime endDate)
    {
        await _subscriptionRepository.AddSubscription(userEmail, subscriptionType, startDate, endDate);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<SubscriptionResponse> GetSubscriptionsByEmailAsync(string email)
    {
        var subscriptions = await _subscriptionRepository.FindByEmailAsync(email);
        if (subscriptions is null)
        {
            return new SubscriptionResponse(false, $"user with email {email} was not found", null);
        }
        if (!subscriptions.Any())
        {
            return new SubscriptionResponse(true, null, new List<Subscription>());
        }

        return new SubscriptionResponse(true, null, subscriptions.ToList());
    }
}
