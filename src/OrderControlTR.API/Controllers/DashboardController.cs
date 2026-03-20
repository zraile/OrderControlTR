using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderControlTR.Application.DTOs.Dashboard;
using OrderControlTR.Domain.Enums;
using OrderControlTR.Infrastructure.Persistence;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/dashboard")]
[Authorize(Roles = "Admin,Manager")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;
    private const int NoBranchFilter = 0;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary([FromQuery] int branchId)
    {
        var today = DateTime.UtcNow.Date;
        var tomorrow = today.AddDays(1);

        var todaySales = await _context.Payments
            .Where(p => p.PaymentDate >= today && p.PaymentDate < tomorrow)
            .SumAsync(p => (decimal?)p.Amount) ?? 0;

        var todayOrderCount = await _context.Orders
            .Where(o => o.InsertDate >= today && o.InsertDate < tomorrow)
            .CountAsync();

        var totalTableCount = await _context.Tables
            .Where(t => t.BranchId == branchId || branchId == NoBranchFilter)
            .CountAsync();

        var occupiedTableCount = await _context.Tables
            .Where(t => (t.BranchId == branchId || branchId == NoBranchFilter) && t.Status == TableStatus.Occupied)
            .CountAsync();

        return Ok(new DashboardSummaryDto
        {
            TodaySales = todaySales,
            TodayOrderCount = todayOrderCount,
            TotalTableCount = totalTableCount,
            OccupiedTableCount = occupiedTableCount,
            TableOccupancyRate = totalTableCount > 0
                ? Math.Round((decimal)occupiedTableCount / totalTableCount * 100, 2)
                : 0
        });
    }

    [HttpGet("sales-by-date-range")]
    public async Task<IActionResult> GetSalesByDateRange([FromQuery] DateRangeRequestDto request)
    {
        var sales = await _context.Payments
            .Where(p => p.PaymentDate >= request.StartDate && p.PaymentDate <= request.EndDate)
            .GroupBy(p => p.PaymentDate.Date)
            .Select(g => new SalesByDateRangeDto
            {
                Date = g.Key,
                TotalSales = g.Sum(p => p.Amount),
                OrderCount = g.Select(p => p.OrderId).Distinct().Count()
            })
            .OrderBy(s => s.Date)
            .ToListAsync();

        return Ok(sales);
    }

    [HttpGet("payment-types-summary")]
    public async Task<IActionResult> GetPaymentTypesSummary([FromQuery] DateRangeRequestDto request)
    {
        var summary = await _context.Payments
            .Where(p => p.PaymentDate >= request.StartDate && p.PaymentDate <= request.EndDate)
            .GroupBy(p => p.PaymentType)
            .Select(g => new PaymentTypesSummaryDto
            {
                PaymentType = g.Key,
                TotalAmount = g.Sum(p => p.Amount),
                Count = g.Count()
            })
            .ToListAsync();

        return Ok(summary);
    }

    [HttpGet("tables-status-summary")]
    public async Task<IActionResult> GetTablesStatusSummary([FromQuery] int branchId)
    {
        var summary = await _context.Tables
            .Where(t => t.BranchId == branchId || branchId == NoBranchFilter)
            .GroupBy(t => t.Status)
            .Select(g => new TableStatusSummaryDto
            {
                Status = g.Key,
                Count = g.Count()
            })
            .ToListAsync();

        return Ok(summary);
    }
}
