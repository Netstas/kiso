using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? OrderId { get; set; } = null;

    public int? ProductId { get; set; } = null;

    public int? Quantity { get; set; } = null;

    public decimal? Price { get; set; } = null;

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
