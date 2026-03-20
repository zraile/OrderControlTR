using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Application.DTOs.Order;
using OrderControlTR.Domain.Entities;
using OrderControlTR.Domain.Enums;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public OrderController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _uow.Orders.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var order = await _uow.Orders.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(_mapper.Map<OrderDto>(order));
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var orders = await _uow.Orders.FindAsync(o =>
            o.Status != OrderStatus.Delivered && o.Status != OrderStatus.Cancelled);
        return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
    }

    [HttpGet("by-table/{tableId}")]
    public async Task<IActionResult> GetByTable(int tableId)
    {
        var orders = await _uow.Orders.FindAsync(o => o.TableId == tableId);
        return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        var entity = _mapper.Map<Order>(dto);
        await _uow.Orders.AddAsync(entity);
        await _uow.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<OrderDto>(entity));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto dto)
    {
        var entity = await _uow.Orders.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _mapper.Map(dto, entity);
        await _uow.Orders.UpdateAsync(entity);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<OrderDto>(entity));
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDto dto)
    {
        var entity = await _uow.Orders.GetByIdAsync(id);
        if (entity == null) return NotFound();
        entity.Status = dto.Status;
        await _uow.Orders.UpdateAsync(entity);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<OrderDto>(entity));
    }
}
