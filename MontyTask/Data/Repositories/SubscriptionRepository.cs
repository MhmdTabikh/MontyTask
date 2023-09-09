using Microsoft.EntityFrameworkCore;
using MontyTask.Data.Models;

namespace MontyTask.Data.Repositories;



//if the repositories are to be scalable,interface would then be in an independent file
public interface ISubscriptionRepository
{
    Task<List<Subscription>?> FindByEmailAsync(string email);
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
    public async Task<List<Subscription>?> FindByEmailAsync(string email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user is null)
            return null;

        var q = await _context.Subscriptions.ToListAsync();
        var subscriptions = await _context.Subscriptions.Where(x=>x.User.Id == user.Id).ToListAsync();

        return subscriptions;
    }
}
