using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University.Database;
using University.Database.Entities;
using University.DTOs;
using University.Helper;

namespace University.Controllers;

[Route("api/[controller]")]
public class FacultyController : CustomBaseController
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public FacultyController(Context context, IMapper mapper)
            : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("[action]")]
    [ProducesResponseType(typeof(List<FacultyDTO>), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<List<FacultyDTO>>> Get()
        => await Get<Faculty, FacultyDTO>();

    [HttpGet("[action]/{id:int}")]
    [ProducesResponseType(typeof(FacultyDTO), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult<FacultyDTO>> Get(int id)
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var faculty = await _context.Faculties
            .AsNoTracking()
            .Include(s => s.Careers.Where(s => s.State))
            .FirstOrDefaultAsync(s => s.Id == id && s.State);

        if (faculty is null)
            return NotFound("Valor no econtrado");

        return _mapper.Map<FacultyDTO>(faculty);
    }

    [HttpPost("[action]")]
    [ProducesResponseType(typeof(FacultyDTO), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Post([FromBody] FacultyCreationDTO creationDTO)
        => await Post<FacultyCreationDTO, Faculty, FacultyDTO>(creationDTO);

    [HttpPut("[action]/{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Put(int id, [FromBody] FacultyCreationDTO creationDTO)
        => await Put<FacultyCreationDTO, Faculty>(id, creationDTO);

    [HttpDelete("[action]/{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Delete(int id, [FromBody] FacultyRemoveDTO? removeDTO)
    {
        if (!ModelState.IsValid)
            return ValidationHelper.HandleValidationErrors(this);

        var faculty = await _context.Faculties
            .Include(s => s.Careers.Where(s => s.State))
            .FirstOrDefaultAsync(s => s.Id == id && s.State);

        if (faculty is null)
            return NotFound("Valor no encontrado");

        if (id == removeDTO?.FacultyTransferId)
            return BadRequest("Id de transferencia debe ser diferente del Id de eliminación");

        if (removeDTO?.FacultyTransferId != 0 && faculty.Careers.Any())
        {
            var toTransfer = await _context.Faculties
                .Include(s => s.Careers.Where(s => s.State))
                .FirstOrDefaultAsync(s => s.State && s.Id == removeDTO.FacultyTransferId);

            if (toTransfer is null)
                return NotFound("Facultad para transferencia no encontrada");

            foreach (var career in faculty.Careers)
            {
                toTransfer.Careers.Add(career);
            }

            toTransfer.ModificationDate = DateTime.Now;
            _context.Entry(toTransfer).State = EntityState.Modified;
        }

        faculty.State = false;
        _context.Entry(faculty).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return Ok("Eliminado correctamente");
    }

    [HttpPut("[action]/{id:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    public async Task<ActionResult> Restore(int id) => await Restore<Faculty>(id);
}

