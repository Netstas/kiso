using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class ProductCategory
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Url { get; set; }

    public int CategorySort { get; set; }

    public string? TitleIntroduce { get; set; }

    public string? Description { get; set; }

    public bool CategoryActive { get; set; }

    public int? ParentId { get; set; }

    public bool ShowMenu { get; set; }

    public bool ShowHome { get; set; }

    public string? TitleMeta { get; set; }

    public string? DescriptionMeta { get; set; }

    public string? Image { get; set; }

    public string? CoverImage { get; set; }

    public string? Body { get; set; }

    public bool Hot { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
