using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Application.DTOs.Restaurant;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RestaurantController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _uow.Restaurants.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<RestaurantDto>>(restaurants));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await _uow.Restaurants.GetByIdAsync(id);
        if (restaurant == null) return NotFound();
        return Ok(_mapper.Map<RestaurantDto>(restaurant));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);
        await _uow.Restaurants.AddAsync(restaurant);
        await _uow.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, _mapper.Map<RestaurantDto>(restaurant));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRestaurantDto dto)
    {
        var restaurant = await _uow.Restaurants.GetByIdAsync(id);
        if (restaurant == null) return NotFound();
        _mapper.Map(dto, restaurant);
        await _uow.Restaurants.UpdateAsync(restaurant);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<RestaurantDto>(restaurant));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var restaurant = await _uow.Restaurants.GetByIdAsync(id);
        if (restaurant == null) return NotFound();
        await _uow.Restaurants.DeleteAsync(restaurant);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}
