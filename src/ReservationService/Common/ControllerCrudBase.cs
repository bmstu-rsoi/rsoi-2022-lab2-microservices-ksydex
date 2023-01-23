using System.Linq.Expressions;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationService.Data;
using ReservationService.Data.Filters;
using SharedKernel.Common.AbstractClasses;
using SharedKernel.Extensions;

namespace ReservationService.Common;

[ApiController]
[Route("api/v1/[controller]")]
public abstract class ControllerCrudBase<TEntity, TDto, TFilter> : ControllerBase
    where TEntity : EntityBase, new()
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _dbContext;

    protected virtual Expression<Func<TEntity, bool>>? UidPredicate(Guid uId) => null;
    protected bool IsUidSupported => UidPredicate(Guid.NewGuid()) != null;
    protected virtual Guid? GetUid(TEntity e) => null;

    protected virtual void SetUid(TEntity entity, Guid uId)
    {
    }

    public ControllerCrudBase(IMapper mapper, AppDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TDto>> GetByIdAsync(string id)
    {
        int? intId = int.TryParse(id, out var asInt) ? asInt : null;
        Guid? guid = Guid.TryParse(id, out var asGuid) ? asGuid : null;

        if (guid == null) return BadRequest("UID is in wrong format");
        if (intId == null && !IsUidSupported) return BadRequest("Entity doesn't support UID");

        var e = await AttachEagerLoadingStrategyToQueryable(_dbContext.Set<TEntity>().AsNoTracking())
            .FirstOrDefaultAsync(intId == null ? UidPredicate(guid.Value)! : x => x.Id == intId);

        if (e == null) return NotFound();

        return Ok(_mapper.Map<TDto>(e));
    }


    [HttpGet]
    public async Task<ActionResult<PaginationModel<TDto>>> GetAllAsync([FromQuery] TFilter filter, [FromQuery] int size = 10,
        [FromQuery] int page = 1)
    {
        Console.WriteLine("Filters: " + JsonSerializer.Serialize(filter));
        var q = AttachEagerLoadingStrategyToQueryable(
            AttachFilterToQueryable(_dbContext.Set<TEntity>(), filter)
                .OrderByDescending(x => x.Id));

        var lst = await q
            .Page(page, size)
            .ToListAsync();

        
        return Ok(new PaginationModel<TDto>
        {
            Page = page,
            PageSize = size,
            TotalElements = await q.CountAsync(),
            Items = _mapper.Map<List<TDto>>(lst)
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] TDto dto)
    {
        var e = new TEntity();
        MapDtoToEntity(e, dto);

        if (IsUidSupported) SetUid(e, Guid.NewGuid());

        await _dbContext.AddAsync(e);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction("GetById", new { id = e.Id }, e);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<TDto>> UpdateAsync([FromBody] TDto dto, string id)
    {
        int? intId = int.TryParse(id, out var asInt) ? asInt : null;
        Guid? guid = Guid.TryParse(id, out var asGuid) ? asGuid : null;

        if (guid == null) return BadRequest("UID is in wrong format");
        if (intId == null && !IsUidSupported) return BadRequest("Entity doesn't support UID");
        
        var e = await _dbContext.Set<TEntity>()
            .FirstOrDefaultAsync(intId == null ? UidPredicate(guid.Value)! : x => x.Id == intId);

        if (e == null) return NotFound();

        MapDtoToEntity(e, dto);

        await _dbContext.SaveChangesAsync();
        _dbContext.Entry(e).State = EntityState.Detached;

        return await GetByIdAsync(id);
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

    protected virtual IQueryable<TEntity> AttachFilterToQueryable(IQueryable<TEntity> q, TFilter f)
        => q;

    protected virtual IQueryable<TEntity> AttachEagerLoadingStrategyToQueryable(IQueryable<TEntity> q)
        => q;
}