using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models;

public partial class Prijava
{
    public int Id { get; set; }

    public string Naslov { get; set; } = null!;

    public string Opis { get; set; } = null!;

    public DateTime DatumPrijave { get; set; }

    public DateTime? DatumPopravka { get; set; }

    public int StatusId { get; set; }

    public int KategorijaId { get; set; }

    public int PrioritetId { get; set; }

    public virtual Kategorija Kategorija { get; set; } = null!;

    public virtual ICollection<PrijavaFileUpload> PrijavaFileUploads { get; set; } = new List<PrijavaFileUpload>();

    public virtual Prioritet Prioritet { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<UserPrijava> UserPrijavas { get; set; } = new List<UserPrijava>();
}
