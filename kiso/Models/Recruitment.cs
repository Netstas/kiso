using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Recruitment
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime CreateDate { get; set; }

    public string InfoFullName { get; set; } = null!;

    public string InfoAddress { get; set; } = null!;

    public string InfoMobile { get; set; } = null!;

    public string InfoEmail { get; set; } = null!;

    public string? InfoBody { get; set; }

    public int? ArticleId { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
