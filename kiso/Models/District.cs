using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class District
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Sort { get; set; }

    public bool Active { get; set; }

    public string? Prefix { get; set; }

    public int CityId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
