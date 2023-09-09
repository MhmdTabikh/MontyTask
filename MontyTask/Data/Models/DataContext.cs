using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MontyTask.Services;

namespace MontyTask.Data.Models;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

/// using this class to avoid injecting IPasswordHasher into DataContext
public class DatabaseSeed
{
    public static async Task SeedAsync(DataContext context, IPasswordHasher passwordHasher)
    {
        bool isNewlyCreated = context.Database.EnsureCreated();

        if (isNewlyCreated)
        {
            var users = new List<User>
        {
            new User {
                Id=1,
                Email = "mhamadtbkh@gmail.com",
                Password = passwordHasher.HashPassword("mhamadtbkh@gmail.com")
            },
            new User {
                Id=2,
                Email = "monty@monty.com",
                Password = passwordHasher.HashPassword("monty@monty.com")
            }
        };
            var subscriptions = new List<Subscription>
        {
            new Subscription {
                Id=1,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                SubscriptionType = SubscriptionType.Standard,
                User = users[0]
            }
        };
            context.Users.AddRange(users);
            await context.SaveChangesAsync();
        }
    }
}
