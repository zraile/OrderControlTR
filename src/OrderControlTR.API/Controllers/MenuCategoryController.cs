using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Application.DTOs.MenuCategory;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/menu-categories")]
[Authorize]
public class MenuCategoryController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public MenuCategoryController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _uow.MenuCategories.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<MenuCategoryDto>>(items));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _uow.MenuCategories.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(_mapper.Map<MenuCategoryDto>(item));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create([FromBody] CreateMenuCategoryDto dto)
    {
        var entity = _mapper.Map<MenuCategory>(dto);
        await _uow.MenuCategories.AddAsync(entity);
        await _uow.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<MenuCategoryDto>(entity));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuCategoryDto dto)
    {
        var entity = await _uow.MenuCategories.GetByIdAsync(id);
        if (entity == null) return NotFound();
        _mapper.Map(dto, entity);
        await _uow.MenuCategories.UpdateAsync(entity);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<MenuCategoryDto>(entity));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _uow.MenuCategories.GetByIdAsync(id);
        if (entity == null) return NotFound();
        await _uow.MenuCategories.DeleteAsync(entity);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}
