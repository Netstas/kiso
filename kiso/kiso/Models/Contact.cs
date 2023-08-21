using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Body { get; set; }

    public DateTime CreateDate { get; set; }
}
