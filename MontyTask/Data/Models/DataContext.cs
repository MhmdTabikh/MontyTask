using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MontyTask.Services;

namespace MontyTask.Data.Models;

//ToDo : whole Data folder to be in another  project MontyTask.Data
public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                SubscriptionType = SubscriptionType.Standard,
                User = users[0]
            }
        };
            try
            {
                context.Users.AddRange(users);
                context.Subscriptions.AddRange(subscriptions);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }

        }
    }
}
