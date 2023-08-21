using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Article
{
    public int Id { get; set; }

    public string Subject { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? Body { get; set; }

    public string? Image { get; set; }

    public DateTime CreateDate { get; set; }

    public int View { get; set; }

    public int ArticleCategoryId { get; set; }

    public bool Active { get; set; }

    public bool Home { get; set; }

    public bool Menu { get; set; }

    public string? Url { get; set; }

    public string? TitleMeta { get; set; }

    public string? DescriptionMeta { get; set; }

    public bool Hot { get; set; }

    public int? RecruitmentId { get; set; }

    public virtual ArticleCategory ArticleCategory { get; set; } = null!;

    public virtual Recruitment? Recruitment { get; set; }
}
