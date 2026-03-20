using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Application.DTOs.Table;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/tables")]
[Authorize]
public class TableController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public TableController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tables = await _uow.Tables.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<TableDto>>(tables));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var table = await _uow.Tables.GetByIdAsync(id);
        if (table == null) return NotFound();
        return Ok(_mapper.Map<TableDto>(table));
    }

    [HttpGet("by-branch/{branchId}")]
    public async Task<IActionResult> GetByBranch(int branchId)
    {
        var tables = await _uow.Tables.FindAsync(t => t.BranchId == branchId);
        return Ok(_mapper.Map<IEnumerable<TableDto>>(tables));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create([FromBody] CreateTableDto dto)
    {
        var entity = _mapper.Map<Table>(dto);
        await _uow.Tables.AddAsync(entity);
        await _uow.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<TableDto>(entity));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTableDto dto)
    {
        var entity = await _uow.Tables.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _mapper.Map(dto, entity);
        await _uow.Tables.UpdateAsync(entity);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<TableDto>(entity));
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateTableStatusDto dto)
    {
        var entity = await _uow.Tables.GetByIdAsync(id);
        if (entity == null) return NotFound();
        entity.Status = dto.Status;
        await _uow.Tables.UpdateAsync(entity);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<TableDto>(entity));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _uow.Tables.GetByIdAsync(id);
        if (entity == null) return NotFound();
        await _uow.Tables.DeleteAsync(entity);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}
