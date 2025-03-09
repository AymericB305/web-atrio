using System;
using System.Collections.Generic;

namespace Back.Domain;

public partial class Person
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public virtual ICollection<PersonJob> PersonJobs { get; set; } = new List<PersonJob>();
}
