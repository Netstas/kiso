using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Material
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Sort { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
