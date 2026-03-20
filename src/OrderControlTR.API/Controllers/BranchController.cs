using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderControlTR.Application.Common.Interfaces;
using OrderControlTR.Application.DTOs.Branch;
using OrderControlTR.Domain.Entities;

namespace OrderControlTR.API.Controllers;

[ApiController]
[Route("api/branches")]
[Authorize]
public class BranchController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public BranchController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var branches = await _uow.Branches.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<BranchDto>>(branches));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var branch = await _uow.Branches.GetByIdAsync(id);
        if (branch == null) return NotFound();
        return Ok(_mapper.Map<BranchDto>(branch));
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create([FromBody] CreateBranchDto dto)
    {
        var branch = _mapper.Map<Branch>(dto);
        await _uow.Branches.AddAsync(branch);
        await _uow.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = branch.Id }, _mapper.Map<BranchDto>(branch));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBranchDto dto)
    {
        var branch = await _uow.Branches.GetByIdAsync(id);
        if (branch == null) return NotFound();
        _mapper.Map(dto, branch);
        await _uow.Branches.UpdateAsync(branch);
        await _uow.SaveChangesAsync();
        return Ok(_mapper.Map<BranchDto>(branch));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var branch = await _uow.Branches.GetByIdAsync(id);
        if (branch == null) return NotFound();
        await _uow.Branches.DeleteAsync(branch);
        await _uow.SaveChangesAsync();
        return NoContent();
    }
}
