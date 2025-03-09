namespace Back.Features.People.AddJobToPerson;

public record AddJobToPersonRequest(string CompanyName, string Position, DateOnly StartDate, DateOnly? EndDate);
