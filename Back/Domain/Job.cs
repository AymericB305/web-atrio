using System;
using System.Collections.Generic;

namespace Back.Domain;

public partial class Job
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Position { get; set; } = null!;

    public virtual ICollection<PersonJob> PersonJobs { get; set; } = new List<PersonJob>();
}
