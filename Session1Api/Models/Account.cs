using System;
using System.Collections.Generic;

namespace Session1Api.Models;

public partial class Account
{
    public Guid AccountId { get; set; }

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public byte Status { get; set; }

    public string? Gender { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateTimeOffset JoinDateTime { get; set; }

    public byte MembershipStatus { get; set; }

    public DateTimeOffset? LastPurchaseDateTime { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
