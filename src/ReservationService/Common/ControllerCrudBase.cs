using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationService.Data;
using SharedKernel.Common.AbstractClasses;

namespace ReservationService.Common;

[ApiController]
[Route("api/v1/[controller]")]
public class ControllerCrudBase<TEntity, TDto> : ControllerBase
where TEntity: EntityBase, new()
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;

    public ControllerCrudBase(IMapper mapper, AppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TDto>> GetByIdAsync(int id)
    {
        var e = await _dbContext.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (e == null) return NotFound();
        
        return Ok(_mapper.Map<TDto>(e));
    }

    [HttpGet]
    public async Task<ActionResult<List<TDto>>> GetAllAsync()
    {
        var lst = await _dbContext.Set<TEntity>()
            .ToListAsync();
        
        return Ok(_mapper.Map<List<TDto>>(lst));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TDto dto)
    {
        var e = new TEntity();
        MapDtoToEntity(e, dto);

        await _dbContext.AddAsync(e);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("GetById", new { id = e.Id }, e);
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<TDto>> UpdateAsync([FromBody] TDto dto, int id)
    {
        var e = await _dbContext.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (e == null) return NotFound();

        MapDtoToEntity(e, dto);

        await _dbContext.SaveChangesAsync();
        
        return Ok(_mapper.Map<TDto>(e));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveAsync(int id)
    {
        var e = await _dbContext.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (e == null) return NotFound();

        _dbContext.Remove(e);
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }

    protected virtual void MapDtoToEntity(TEntity e, TDto dto)
    {
    }
}