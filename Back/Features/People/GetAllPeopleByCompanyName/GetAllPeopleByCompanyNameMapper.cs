using Back.Common;
using Back.Domain;

namespace Back.Features.People.GetAllPeopleByCompanyName;

public static class GetAllPeopleByCompanyNameMapper
{
    public static GetAllPeopleByCompanyNameResponse ToGetAllPeopleByCompanyNameResponse(this IEnumerable<Person> people)
    {
        return new GetAllPeopleByCompanyNameResponse(people.Select(p => p.ToResponse()));
    }

    private static GetPersonResponse ToResponse(this Person person)
    {
        return new GetPersonResponse(
            person.Id,
            person.FirstName,
            person.LastName,
            Helper.GetAge(person.BirthDate)
        );
    }
}
