using Microsoft.EntityFrameworkCore;
using MontyTask.Data.Models;

namespace MontyTask.Data.Repositories;



//if the repositories are to be scalable,interface would then be in an independent file
public interface ISubscriptionRepository
{
    Task<List<Subscription>?> FindByEmailAsync(string email);
    Task AddSubscription(string email,SubscriptionType subscription,DateTime startDate,DateTime endDate);// can a user have multiple subscriptions ? I will suppose so
}

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly DataContext _context;
    private readonly IUserRepository _userRepository;

    public SubscriptionRepository(DataContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }

    public async Task AddSubscription(string email, SubscriptionType subscription, DateTime startDate, DateTime endDate)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
            return;

        var subscriptionFound = await _context.Subscriptions.FirstOrDefaultAsync(x => x.SubscriptionType == subscription && x.User == user);
        if (subscriptionFound is not null)
        {
            //update date
            subscriptionFound.StartDate = startDate;
            subscriptionFound.EndDate = endDate;
        }
        else
        {
            await _context.Subscriptions.AddAsync(new Subscription()
            {
                User = user,
                SubscriptionType = subscription,
                StartDate = startDate,
                EndDate = endDate
            });
        }

    }

    public async Task<List<Subscription>?> FindByEmailAsync(string email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
            return null;

        var subscriptions = await _context.Subscriptions.Where(x=>x.User.Id == user.Id).ToListAsync();

        return subscriptions;
    }
}
