using OrderControlTR.Domain.Entities;
using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Domain.Tests.Entities;

public class TableTests
{
    [Fact]
    public void Table_DefaultStatus_ShouldBeAvailable()
    {
        var table = new Table();
        Assert.Equal(TableStatus.Available, table.Status);
    }

    [Fact]
    public void Table_IsActive_DefaultShouldBeTrue()
    {
        var table = new Table();
        Assert.True(table.IsActive);
    }
}
