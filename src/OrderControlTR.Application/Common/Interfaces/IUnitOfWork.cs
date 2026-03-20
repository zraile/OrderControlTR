using OrderControlTR.Domain.Entities;

namespace OrderControlTR.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Restaurant> Restaurants { get; }
    IRepository<Branch> Branches { get; }
    IRepository<User> Users { get; }
    IRepository<Role> Roles { get; }
    IRepository<UserRole> UserRoles { get; }
    IRepository<MenuCategory> MenuCategories { get; }
    IRepository<MenuItem> MenuItems { get; }
    IRepository<Table> Tables { get; }
    IRepository<Order> Orders { get; }
    IRepository<OrderItem> OrderItems { get; }
    IRepository<Payment> Payments { get; }
    IRepository<Customer> Customers { get; }
    Task<int> SaveChangesAsync();
}
