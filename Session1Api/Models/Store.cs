using System;
using System.Collections.Generic;

namespace Session1Api.Models;

public partial class Store
{
    public Guid StoreId { get; set; }

    public string DisplayName { get; set; } = null!;

    public byte ChannelType { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
