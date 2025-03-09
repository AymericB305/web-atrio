using Back.Domain;

namespace Back.Features.People.AddPerson;

public static class AddPersonMapper
{
    public static Person ToEntity(this AddPersonRequest request)
    {
        return new Person
        {
            LastName = request.LastName,
            FirstName = request.FirstName,
            BirthDate = request.BirthDate,
        };
    }

    public static AddPersonResponse ToAddPersonResponse(this Person person)
    {
        return new AddPersonResponse(
            person.Id,
            person.LastName,
            person.FirstName,
            person.BirthDate
        );
    }
}
