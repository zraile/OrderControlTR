using Microsoft.EntityFrameworkCore;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Domain.Common;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ITenantService _tenantService;

    public AppDbContext(DbContextOptions<AppDbContext> options, ITenantService tenantService)
        : base(options)
    {
        _tenantService = tenantService;
    }

    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<MenuCategory> MenuCategories => Set<MenuCategory>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Table> Tables => Set<Table>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
                var property = System.Linq.Expressions.Expression.Property(parameter, "IsDeleted");
                var falseValue = System.Linq.Expressions.Expression.Constant(false);
                var body = System.Linq.Expressions.Expression.Equal(property, falseValue);
                var lambda = System.Linq.Expressions.Expression.Lambda(body, parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.TaxNumber).HasMaxLength(50);
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.HasOne(e => e.Restaurant)
                .WithMany(r => r.Branches)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();
            entity.HasOne(e => e.Restaurant)
                .WithMany(r => r.Users)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MenuCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasOne(e => e.Restaurant)
                .WithMany(r => r.MenuCategories)
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(10, 2);
            entity.HasOne(e => e.Category)
                .WithMany(c => c.MenuItems)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Branch)
                .WithMany(b => b.Tables)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalAmount).HasPrecision(10, 2);
            entity.HasOne(e => e.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(e => e.TableId)
                .OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(e => e.Branch)
                .WithMany(b => b.Orders)
                .HasForeignKey(e => e.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UnitPrice).HasPrecision(10, 2);
            entity.Property(e => e.TotalPrice).HasPrecision(10, 2);
            entity.HasOne(e => e.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.MenuItem)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(e => e.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.HasOne(e => e.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.HasOne(e => e.Restaurant)
                .WithMany()
                .HasForeignKey(e => e.RestaurantId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin", InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Role { Id = 2, Name = "Manager", InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Role { Id = 3, Name = "Waiter", InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Role { Id = 4, Name = "Chef", InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Role { Id = 5, Name = "Cashier", InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Role { Id = 6, Name = "Courier", InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );

        modelBuilder.Entity<Restaurant>().HasData(
            new Restaurant
            {
                Id = 1,
                Name = "Demo Restaurant",
                Address = "123 Main Street, Istanbul",
                Phone = "+90 212 000 0000",
                TaxNumber = "1234567890",
                IsActive = true,
                InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Branch>().HasData(
            new Branch
            {
                Id = 1,
                RestaurantId = 1,
                Name = "Main Branch",
                Address = "123 Main Street, Istanbul",
                Phone = "+90 212 000 0001",
                IsActive = true,
                InsertDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}
