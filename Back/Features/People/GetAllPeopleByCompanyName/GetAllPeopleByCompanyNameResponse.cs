namespace Back.Features.People.GetAllPeopleByCompanyName;

public record GetAllPeopleByCompanyNameResponse(IEnumerable<GetPersonResponse> People);

public record GetPersonResponse(int Id, string FirstName, string LastName, int Age);
