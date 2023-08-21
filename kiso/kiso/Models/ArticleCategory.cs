using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class ArticleCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Url { get; set; }

    public int CategorySort { get; set; }

    public bool CategoryActive { get; set; }

    public int? ParentId { get; set; }

    public bool ShowMenu { get; set; }

    public bool ShowFooter { get; set; }

    public string? TitleMeta { get; set; }

    public string? DescriptionMeta { get; set; }

    public string? Image { get; set; }

    public int TypePost { get; set; }

    public bool Home { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();
}
