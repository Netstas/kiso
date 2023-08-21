using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Cart
{
    public int RecordId { get; set; }

    public string? CartId { get; set; }

    public int ProductId { get; set; }

    public decimal? Price { get; set; }

    public int Count { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal? TotalPrice { get; set; }

    public decimal? Total { get; set; }

    public virtual Product Product { get; set; } = null!;
}
