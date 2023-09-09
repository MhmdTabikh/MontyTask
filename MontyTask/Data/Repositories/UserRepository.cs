using Microsoft.EntityFrameworkCore;
using MontyTask.Data.Models;

namespace MontyTask.Data.Repositories;


//if the repositories are to be scalable,interface would then be in an independent file
public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> FindByEmailAsync(string email);
}

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

}
