using OrderControlTR.Domain.Entities;
using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Domain.Tests.Entities;

public class OrderTests
{
    [Fact]
    public void Order_DefaultStatus_ShouldBeNew()
    {
        var order = new Order();
        Assert.Equal(OrderStatus.New, order.Status);
    }

    [Fact]
    public void Order_DefaultType_ShouldBeDineIn()
    {
        var order = new Order();
        Assert.Equal(OrderType.DineIn, order.OrderType);
    }

    [Fact]
    public void Order_IsActive_DefaultShouldBeTrue()
    {
        var order = new Order();
        Assert.True(order.IsActive);
    }

    [Fact]
    public void Order_IsDeleted_DefaultShouldBeFalse()
    {
        var order = new Order();
        Assert.False(order.IsDeleted);
    }
}
