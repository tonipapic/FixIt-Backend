using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models.Database;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual UserLocalAuth? UserLocalAuth { get; set; }

    public virtual ICollection<UserOauth> UserOauths { get; set; } = new List<UserOauth>();

    public virtual ICollection<UserPrijava> UserPrijavas { get; set; } = new List<UserPrijava>();
}
