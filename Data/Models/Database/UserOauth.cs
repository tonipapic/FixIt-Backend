using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models.Database;

public partial class UserOauth
{
    public int Id { get; set; }

    public string Provider { get; set; } = null!;

    public string? ProviderUserId { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
