namespace Back.Features.People.GetPersonsJobs;

public class GetPersonsJobsResponse(IEnumerable<JobResponse> Jobs);

public record JobResponse(string CompanyName, string Position, DateOnly StartDate, DateOnly? EndDate);