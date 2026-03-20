using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private bool _disposed;

    public IRepository<Restaurant> Restaurants { get; }
    public IRepository<Branch> Branches { get; }
    public IRepository<User> Users { get; }
    public IRepository<Role> Roles { get; }
    public IRepository<UserRole> UserRoles { get; }
    public IRepository<MenuCategory> MenuCategories { get; }
    public IRepository<MenuItem> MenuItems { get; }
    public IRepository<Table> Tables { get; }
    public IRepository<Order> Orders { get; }
    public IRepository<OrderItem> OrderItems { get; }
    public IRepository<Payment> Payments { get; }
    public IRepository<Customer> Customers { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Restaurants = new Repository<Restaurant>(context);
        Branches = new Repository<Branch>(context);
        Users = new Repository<User>(context);
        Roles = new Repository<Role>(context);
        UserRoles = new Repository<UserRole>(context);
        MenuCategories = new Repository<MenuCategory>(context);
        MenuItems = new Repository<MenuItem>(context);
        Tables = new Repository<Table>(context);
        Orders = new Repository<Order>(context);
        OrderItems = new Repository<OrderItem>(context);
        Payments = new Repository<Payment>(context);
        Customers = new Repository<Customer>(context);
    }

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
            _context.Dispose();
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
