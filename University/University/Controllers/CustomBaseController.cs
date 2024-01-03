using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Database;
using University.Database.Entities;
using University.Helper;

namespace University.Controllers;

public class CustomBaseController : ControllerBase
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public CustomBaseController(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    protected async Task<List<TDTO>> Get<TEntity, TDTO>()
        where TEntity : class, IInherent
    {
        var entities = await _context.Set<TEntity>()
            .AsNoTracking()
            .Where(s => s.State)
            .ToListAsync();

        return _mapper.Map<List<TDTO>>(entities);
    }

    protected async Task<ActionResult<TDTO>> Get<TEntity, TDTO>(int id)
        where TEntity : class, IInherent
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var entity = await _context.Set<TEntity>()
            .AsNoTracking()
            .Where(s => s.State)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (entity is null)
            return NotFound("Valor no econtrado");

        return _mapper.Map<TDTO>(entity);
    }

    protected async Task<ActionResult> Post<TCreation, TEntity, TRead>(TCreation creation)
        where TEntity : class, IInherent
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var entity = _mapper.Map<TEntity>(creation);
        entity.CreationDate = DateTime.Now;

        _context.Add(entity);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<TRead>(entity));
    }

    protected async Task<ActionResult> Put<TCreation, TEntity>(int id, TCreation creation)
        where TEntity : class, IInherent
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(s => s.Id == id && s.State);

        if (existingEntity is null)
            return NotFound("Valor no econtrado");

        _mapper.Map(creation, existingEntity);

        existingEntity.ModificationDate = DateTime.Now;

        _context.Entry(existingEntity).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return Ok("Actualizacion finalizada");
    }

    protected async Task<ActionResult> Delete<TEntity>(int id)
        where TEntity : class, IInherent
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(s => s.Id == id && s.State);

        if (entity is null)
            return NotFound("Valor no econtrado");

        entity.State = false;
        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return Ok("Eliminado correctamente");
    }

    protected async Task<ActionResult> Restore<TEntity>(int id)
        where TEntity : class, IInherent
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(s => s.Id == id && !s.State);

        if (entity is null)
            return NotFound("Valor no econtrado");

        entity.State = true;
        _context.Entry(entity).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return Ok("Restaurado correctamente");
    }
}

