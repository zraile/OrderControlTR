using AutoMapper;
using OrderControlTR.Application.DTOs.Auth;
using OrderControlTR.Application.DTOs.Branch;
using OrderControlTR.Application.DTOs.Customer;
using OrderControlTR.Application.DTOs.MenuCategory;
using OrderControlTR.Application.DTOs.MenuItem;
using OrderControlTR.Application.DTOs.Order;
using OrderControlTR.Application.DTOs.OrderItem;
using OrderControlTR.Application.DTOs.Payment;
using OrderControlTR.Application.DTOs.Restaurant;
using OrderControlTR.Application.DTOs.Table;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<CreateRestaurantDto, Restaurant>();
        CreateMap<UpdateRestaurantDto, Restaurant>();

        CreateMap<Branch, BranchDto>();
        CreateMap<CreateBranchDto, Branch>();
        CreateMap<UpdateBranchDto, Branch>();

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src =>
                src.UserRoles.Select(ur => ur.Role.Name).ToList()));

        CreateMap<MenuCategory, MenuCategoryDto>();
        CreateMap<CreateMenuCategoryDto, MenuCategory>();
        CreateMap<UpdateMenuCategoryDto, MenuCategory>();

        CreateMap<MenuItem, MenuItemDto>();
        CreateMap<CreateMenuItemDto, MenuItem>();
        CreateMap<UpdateMenuItemDto, MenuItem>();

        CreateMap<Table, TableDto>();
        CreateMap<CreateTableDto, Table>();
        CreateMap<UpdateTableDto, Table>();

        CreateMap<Order, OrderDto>();
        CreateMap<CreateOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name));
        CreateMap<CreateOrderItemDto, OrderItem>();
        CreateMap<UpdateOrderItemDto, OrderItem>();

        CreateMap<Payment, PaymentDto>();
        CreateMap<CreatePaymentDto, Payment>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();
    }
}
