namespace Back.Features.People.GetAllPeople;

public record GetAllPeopleResponse(IEnumerable<GetPersonResponse> People);

public record GetPersonResponse(int Id, string FirstName, string LastName, int Age, IEnumerable<JobResponse> CurrentJobs);

public record JobResponse(string CompanyName, string Position, DateOnly StartDate);