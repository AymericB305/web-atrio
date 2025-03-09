using Back.Domain;

namespace Back.Features.People.AddJobToPerson;

public static class AddJobToPersonMapper
{
    public static Job GetJob(this AddJobToPersonRequest request)
    {
        return new Job
        {
            CompanyName = request.CompanyName,
            Position = request.Position,
        };
    }

    public static PersonJob ToPersonJob(this AddJobToPersonRequest request, int personId, int jobId)
    {
        return new PersonJob
        {
            IdPerson = personId,
            IdJob = jobId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
        };
    }

    public static AddJobToPersonResponse ToResponse(this PersonJob personJob, string companyName, string position)
    {
        return new AddJobToPersonResponse(
            personJob.IdJob,
            companyName,
            position,
            personJob.StartDate,
            personJob.EndDate
        );
    }
}
