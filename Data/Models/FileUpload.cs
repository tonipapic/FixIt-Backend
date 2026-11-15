using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models;

public partial class FileUpload
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Extension { get; set; } = null!;

    public string Path { get; set; } = null!;

    public DateTime Uploaded { get; set; }

    public virtual ICollection<PrijavaFileUpload> PrijavaFileUploads { get; set; } = new List<PrijavaFileUpload>();
}
