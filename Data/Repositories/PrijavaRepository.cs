using FixIt_Backend.Data.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace FixIt_Backend.Data.Repositories
{
    public class PrijavaRepository
    {
        private readonly FixItContext _fixItContext;
        public PrijavaRepository(FixItContext fixItContext)
        {
            _fixItContext = fixItContext;
        }

        internal async Task<List<Prijava>> GetPrijaveByReporterId(int? id)
        {

           return await _fixItContext.Prijavas
                .Where(p => p.UserPrijavas.Any(p => p.UserId == id)).Select(p => new Prijava ()
            {
                    Id = p.Id,
                    DatumPopravka = p.DatumPopravka,
                    DatumPrijave = p.DatumPrijave,
                    KategorijaId = p.KategorijaId,
                    Kategorija = new()
                    {
                        Id = p.KategorijaId,
                        Name = p.Kategorija.Name,
                        TipKategorije = p.Kategorija.TipKategorije
                    },
                    Naslov = p.Naslov,
                    Opis = p.Opis,
                    UserPrijavas = p.UserPrijavas.Select(p => new UserPrijava()
                    {
                        IsDeleted = p.IsDeleted,
                        IsRepairer = p.IsRepairer,
                        PrijavaId = p.PrijavaId,
                        UserId = p.UserId,
                        User = new()
                        {
                            Id = p.UserId,
                            Name = p.User.Name,
                            Surname = p.User.Surname,
                            Email = p.User.Email,

                        }
                    }).ToList(),
                    PrioritetId = p.PrioritetId,
                    Prioritet = new Prioritet()
                    {
                        Id = p.PrioritetId,
                        Name = p.Prioritet.Name,
                        Weight = p.Prioritet.Weight
                    },
                    StatusId = p.StatusId,
                    Status = new()
                    {
                        Id = p.StatusId,
                        Name = p.Status.Name,
                        
                    }
                    
            }).ToListAsync();
        }
        internal async Task<List<Prijava>> GetAllPrijave()
        {

           return await _fixItContext.Prijavas.Select(p => new Prijava ()
            {
                    Id = p.Id,
                    DatumPopravka = p.DatumPopravka,
                    DatumPrijave = p.DatumPrijave,
                    KategorijaId = p.KategorijaId,
                    Kategorija = new()
                    {
                        Id = p.KategorijaId,
                        Name = p.Kategorija.Name,
                        TipKategorije = p.Kategorija.TipKategorije
                    },
                    Naslov = p.Naslov,
                    Opis = p.Opis,
                    UserPrijavas = p.UserPrijavas.Select(p => new UserPrijava()
                    {
                        IsDeleted = p.IsDeleted,
                        IsRepairer = p.IsRepairer,
                        PrijavaId = p.PrijavaId,
                        UserId = p.UserId,
                        User = new()
                        {
                            Id = p.UserId,
                            Name = p.User.Name,
                            Surname = p.User.Surname,
                            Email = p.User.Email,

                        }
                    }).ToList(),
                    PrioritetId = p.PrioritetId,
                    Prioritet = new Prioritet()
                    {
                        Id = p.PrioritetId,
                        Name = p.Prioritet.Name,
                        Weight = p.Prioritet.Weight
                    },
                    StatusId = p.StatusId,
                    Status = new()
                    {
                        Id = p.StatusId,
                        Name = p.Status.Name,
                        
                    }
                    
            }).ToListAsync();
        }

        internal async Task<Prijava?> CreatePrijava(Prijava prijava)
        {
            throw new NotImplementedException();
        }
    }
}
