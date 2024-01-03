using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Database;
using University.Database.Entities;
using University.DTOs;
using University.Helper;

namespace University.Controllers;

[Route("api/[controller]")]
public class CareerController : CustomBaseController
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public CareerController(Context context, IMapper mapper)
                : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(List<CareerDTO>), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<List<CareerDTO>>> Get()
        => await Get<Career, CareerDTO>();

    [HttpGet("[action]/{id:int}")]
    [ProducesResponseType(typeof(CareerDTO), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<CareerDTO>> Get(int id)
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var career = await _context.Careers
            .AsNoTracking()
            .Include(s => s.Faculty)
            .FirstOrDefaultAsync(s => s.Id == id && s.State);

        if (career is null)
            return NotFound("Valor no econtrado");

        return _mapper.Map<CareerDTO>(career);
    }

    [HttpPost("[action]")]
    [ProducesResponseType(typeof(CareerDTO), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Post([FromBody] CareerCreationDTO creationDTO)
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var exists = await _context.Faculties.AsNoTracking().AnyAsync(s => s.Id == creationDTO.FacultyId && s.State);

        if (!exists)
            return NotFound("Facultad no encontrada");

        return await Post<CareerCreationDTO, Career, CareerDTO>(creationDTO);
    }

    [HttpPut("[action]/{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Put(int id, [FromBody] CareerCreationDTO creationDTO)
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var exists = await _context.Faculties.AsNoTracking().AnyAsync(s => s.Id == creationDTO.FacultyId && s.State);

        if (!exists)
            return NotFound("Facultad no encontrada");

        return await Put<CareerCreationDTO, Career>(id, creationDTO);
    }

    [HttpDelete("[action]/{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Delete(int id) => await Delete<Career>(id);

    [HttpPut("[action]/{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Restore(int id) => await Restore<Career>(id);
}

