using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Application.DTOs.Dashboard;

public class DashboardSummaryDto
{
    public decimal TodaySales { get; set; }
    public int TodayOrderCount { get; set; }
    public int OccupiedTableCount { get; set; }
    public int TotalTableCount { get; set; }
    public decimal TableOccupancyRate { get; set; }
}

public class SalesByDateRangeDto
{
    public DateTime Date { get; set; }
    public decimal TotalSales { get; set; }
    public int OrderCount { get; set; }
}

public class PaymentTypesSummaryDto
{
    public PaymentType PaymentType { get; set; }
    public decimal TotalAmount { get; set; }
    public int Count { get; set; }
}

public class TableStatusSummaryDto
{
    public TableStatus Status { get; set; }
    public int Count { get; set; }
}

public class DateRangeRequestDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int BranchId { get; set; }
}
