using System;
using System.Collections.Generic;

namespace Session1Api.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid? AccountId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public DateTimeOffset OrderDateTime { get; set; }

    public decimal TotalAmount { get; set; }

    public byte Status { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public Guid StoreId { get; set; }

    public decimal? DiscountAmount { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Store Store { get; set; } = null!;
}
