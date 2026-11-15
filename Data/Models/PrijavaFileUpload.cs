using System;
using System.Collections.Generic;

namespace FixIt_Backend.Data.Models;

public partial class PrijavaFileUpload
{
    public int Id { get; set; }

    public int PrijavaId { get; set; }

    public int FileUploadId { get; set; }

    public virtual FileUpload FileUpload { get; set; } = null!;

    public virtual Prijava Prijava { get; set; } = null!;
}
