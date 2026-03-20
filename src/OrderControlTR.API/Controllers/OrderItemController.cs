using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Application.DTOs.OrderItem;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/order-items")]
[Authorize]
public class OrderItemController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public OrderItemController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _uow.OrderItems.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<OrderItemDto>>(items));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderItemDto dto)
    {
        var menuItem = await _uow.MenuItems.GetByIdAsync(dto.MenuItemId);
        if (menuItem == null) return NotFound("Menu item not found.");

        var entity = _mapper.Map<OrderItem>(dto);
        entity.UnitPrice = menuItem.Price;
        entity.TotalPrice = menuItem.Price * dto.Quantity;

        await _uow.OrderItems.AddAsync(entity);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<OrderItemDto>(entity));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderItemDto dto)
    {
        var entity = await _uow.OrderItems.GetByIdAsync(id);
        if (entity == null) return NotFound();
        entity.Quantity = dto.Quantity;
        entity.Notes = dto.Notes;
        entity.Status = dto.Status;
        entity.TotalPrice = entity.UnitPrice * dto.Quantity;
        await _uow.OrderItems.UpdateAsync(entity);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<OrderItemDto>(entity));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _uow.OrderItems.GetByIdAsync(id);
        if (entity == null) return NotFound();
        await _uow.OrderItems.DeleteAsync(entity);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}
