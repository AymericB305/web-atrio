using Back.Common;
using Back.Domain;
using Back.Features.People.AddJobToPerson;
using Back.Features.People.AddPerson;
using Back.Features.People.GetAllPeople;
using Back.Features.People.GetAllPeopleByCompanyName;
using Back.Features.People.GetPersonsJobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back.Features.People;

[ApiController]
[Route("people")]
public class PeopleController : ControllerBase
{
    private readonly WebAtrioContext _context;

    public PeopleController(WebAtrioContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPerson([FromBody] AddPersonRequest request)
    {
        int age = Helper.GetAge(request.BirthDate);
        if (age > 150)
        {
            return BadRequest("L'âge de la personne ne peut pas dépasser 150 ans.");
        }

        var person = await _context.People.AddAsync(request.ToEntity());

        await _context.SaveChangesAsync();

        return Ok(person.Entity.ToAddPersonResponse());
    }

    [HttpGet]
    public async Task<ActionResult<GetAllPeopleResponse>> GetAllPeople()
    {
        var people = await _context.People
            .Include(p => p.PersonJobs.Where(pj => pj.EndDate == null))
                .ThenInclude(pj => pj.Job)
            .OrderBy(p => p.LastName)
            .ToListAsync();

        return Ok(people.ToResponse());
    }

    [HttpPost("{personId}/jobs")]
    public async Task<IActionResult> AddJobToPerson([FromRoute] int personId, [FromBody] AddJobToPersonRequest request)
    {
        var person = await _context.People.FindAsync(personId);
        if (person == null)
        {
            return NotFound("Personne non trouvée.");
        }

        if (request.EndDate.HasValue && request.EndDate < request.StartDate)
        {
            return BadRequest("La date de fin ne peut pas être avant la date de début.");
        }

        var job = await _context.Jobs.AddAsync(request.GetJob());
        await _context.SaveChangesAsync();

        var result = await _context.PersonJobs.AddAsync(request.ToPersonJob(personId, job.Entity.Id));

        await _context.SaveChangesAsync();

        return Ok(result.Entity.ToResponse(job.Entity.CompanyName, job.Entity.Position));
    }

    [HttpGet("{personId}/jobs")]
    public async Task<IActionResult> GetPersonsJobs([FromRoute] int personId, [FromBody] GetPersonsJobsRequest request)
    {
        if (request.EndDate < request.StartDate)
        {
            return BadRequest("La date de fin ne peut pas être avant la date de début.");
        }

        var person = await _context.People
            .Include(j => j.PersonJobs.Where(pj => pj.StartDate <= request.EndDate && (pj.EndDate == null || pj.EndDate >= request.StartDate)))
                .ThenInclude(pj => pj.Job)
            .FirstOrDefaultAsync(p => p.Id == personId);
        if (person == null)
        {
            return NotFound("Personne non trouvée.");
        }

        return Ok(person.ToGetPersonsJobsReponse());
    }

    [HttpGet("by-company/{companyName}")]
    public async Task<IActionResult> GetPeopleByCompany([FromRoute] string companyName)
    {
        var job = await _context.Jobs
            .Include(j => j.PersonJobs)
                .ThenInclude(pj => pj.Person)
            .Where(j => j.CompanyName == companyName)
            .ToListAsync();

        if (job == null)
        {
            return NotFound("Entreprise non trouvée.");
        }

        var people = job.SelectMany(j => j.PersonJobs)
            .Select(j => j.Person)
            .Distinct();

        return Ok(people.ToGetAllPeopleByCompanyNameResponse());
    }
}
