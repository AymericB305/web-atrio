using System;
using System.Collections.Generic;

namespace Back.Domain;

public partial class PersonJob
{
    public int IdPerson { get; set; }

    public int IdJob { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Job Job { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;
}
