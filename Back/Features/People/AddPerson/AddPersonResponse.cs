namespace Back.Features.People.AddPerson;

public record AddPersonResponse(int Id, string LastName, string FirstName, DateOnly BirthDate);
