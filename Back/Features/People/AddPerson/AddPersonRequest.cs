namespace Back.Features.People.AddPerson;

public record AddPersonRequest(string LastName, string FirstName, DateOnly BirthDate);