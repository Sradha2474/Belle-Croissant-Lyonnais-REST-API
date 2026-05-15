using System;
using System.Collections.Generic;

namespace Session1Api.Models;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal Cost { get; set; }

    public string? Description { get; set; }

    public bool Seasonal { get; set; }

    public bool Active { get; set; }

    public DateOnly IntroducedDate { get; set; }

    public string? Ingredients { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
