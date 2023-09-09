using MontyTask.Data.Models;

namespace MontyTask.Data;

public interface IUnitOfWork
{
    Task CompleteAsync();
}
public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}
