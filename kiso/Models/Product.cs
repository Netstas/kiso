using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Wattage { get; set; }

    public string? Voltage { get; set; }

    public string? ColorRenderingIndex { get; set; }

    public string? Guarantee { get; set; }

    public string? Body { get; set; }

    public string? ListImage { get; set; }

    public decimal? Price { get; set; }

    public decimal? PriceSale { get; set; }

    public DateTime CreateDate { get; set; }

    public int Sort { get; set; }

    public bool Active { get; set; }

    public bool Home { get; set; }

    public bool Hot { get; set; }

    public string? Url { get; set; }

    public string? TitleMeta { get; set; }

    public string? DescriptionMeta { get; set; }

    public int ProductCategoryId { get; set; }

    public int? MetarialId { get; set; }

    public string? BaoHanh { get; set; }

    public string? DoiTra { get; set; }

    public string MaSp { get; set; } = null!;

    public bool Stocking { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ProductCategory ProductCategory { get; set; } = null!;

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
