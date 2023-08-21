using System;
using System.Collections.Generic;

namespace kiso.Models;

public partial class Banner
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public string BannerName { get; set; } = null!;

    public string? Slogan { get; set; }

    public string? Image { get; set; }

    public bool Active { get; set; }

    public string? Url { get; set; }

    public int Sort { get; set; }

    public string? Content { get; set; }
}
