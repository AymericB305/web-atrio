using Back.Common;
using Back.Domain;

namespace Back.Features.People.GetAllPeople;

public static class GetAllPeopleMapper
{
    public static GetAllPeopleResponse ToResponse(this IEnumerable<Person> people)
    {
        return new GetAllPeopleResponse(people.Select(p => p.ToGetPersonResponse()));
    }

    public static GetPersonResponse ToGetPersonResponse(this Person person)
    {
        return new GetPersonResponse(
            person.Id,
            person.FirstName,
            person.LastName,
            Helper.GetAge(person.BirthDate),
            person.PersonJobs.Select(pj => pj.ToJobResponse())
        );
    }

    private static JobResponse ToJobResponse(this PersonJob personJob)
    {
        return new JobResponse(
            personJob.Job.CompanyName,
            personJob.Job.Position,
            personJob.StartDate
        );
    }
}
