using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Application.DTOs.Payment;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/payments")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public PaymentController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var payments = await _uow.Payments.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var payment = await _uow.Payments.GetByIdAsync(id);
        if (payment == null) return NotFound();
        return Ok(_mapper.Map<PaymentDto>(payment));
    }

    [HttpGet("by-order/{orderId}")]
    public async Task<IActionResult> GetByOrder(int orderId)
    {
        var payments = await _uow.Payments.FindAsync(p => p.OrderId == orderId);
        return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
    {
        var entity = _mapper.Map<Payment>(dto);
        await _uow.Payments.AddAsync(entity);
        await _uow.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<PaymentDto>(entity));
    }
}
