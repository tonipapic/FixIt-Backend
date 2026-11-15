using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models;

public partial class UserLocalAuth
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? Salt { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
