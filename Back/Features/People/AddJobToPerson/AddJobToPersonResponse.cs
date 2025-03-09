namespace Back.Features.People.AddJobToPerson;

public record AddJobToPersonResponse(int IdJob, string CompanyName, string Position, DateOnly StartDate, DateOnly? EndDate);
