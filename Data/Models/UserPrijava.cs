using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models;

public partial class UserPrijava
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PrijavaId { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsRepairer { get; set; }

    public virtual Prijava Prijava { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
