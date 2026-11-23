using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models.Database;

public partial class Kategorija
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string TipKategorije { get; set; } = null!;

    public virtual ICollection<Prijava> Prijavas { get; set; } = new List<Prijava>();
}
