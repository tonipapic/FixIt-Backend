using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models;

public partial class Prioritet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Weight { get; set; }

    public virtual ICollection<Prijava> Prijavas { get; set; } = new List<Prijava>();
}
