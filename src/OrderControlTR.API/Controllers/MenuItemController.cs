using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Application.DTOs.MenuItem;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/menu-items")]
[Authorize]
public class MenuItemController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public MenuItemController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _uow.MenuItems.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _uow.MenuItems.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(_mapper.Map<MenuItemDto>(item));
    }

    [HttpGet("by-category/{categoryId}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var items = await _uow.MenuItems.FindAsync(m => m.CategoryId == categoryId);
        return Ok(_mapper.Map<IEnumerable<MenuItemDto>>(items));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create([FromBody] CreateMenuItemDto dto)
    {
        var entity = _mapper.Map<MenuItem>(dto);
        await _uow.MenuItems.AddAsync(entity);
        await _uow.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<MenuItemDto>(entity));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuItemDto dto)
    {
        var entity = await _uow.MenuItems.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _mapper.Map(dto, entity);
        await _uow.MenuItems.UpdateAsync(entity);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<MenuItemDto>(entity));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _uow.MenuItems.GetByIdAsync(id);
        if (entity == null) return NotFound();
        await _uow.MenuItems.DeleteAsync(entity);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}
