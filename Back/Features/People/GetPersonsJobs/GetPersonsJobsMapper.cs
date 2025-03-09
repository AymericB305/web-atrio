using Back.Domain;

namespace Back.Features.People.GetPersonsJobs;

public static class GetPersonsJobsMapper
{
    public static GetPersonsJobsResponse ToGetPersonsJobsReponse(this Person person)
    {
        return new GetPersonsJobsResponse(person.PersonJobs.Select(pj => pj.ToGetPersonsJob()));
    }

    private static JobResponse ToGetPersonsJob(this PersonJob personJob)
    {
        return new JobResponse(
            personJob.Job.CompanyName,
            personJob.Job.Position,
            personJob.StartDate,
            personJob.EndDate
        );
    }
}
