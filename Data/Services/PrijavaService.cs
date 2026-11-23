using FixIt_Backend.Data.Models.Database;
using FixIt_Backend.Data.Repositories.Report;
using FixItShared.Data.DTOs.Responses.Prijava;
using Newtonsoft.Json;

namespace FixIt_Backend.Data.Services
{
    public class PrijavaService
    {
        private readonly PrijavaRepository _prijavaRepository;
        public PrijavaService(PrijavaRepository prijavaRepository)
        {
            _prijavaRepository = prijavaRepository;
        }

        internal async Task<List<PrijavaResponseDTO>> GetPrijaveByReporterId(int? id)
        {
            var prijave = await _prijavaRepository.GetPrijaveByReporterId(id);

            var prijavaResponseDTO = prijave.Select(p => new PrijavaResponseDTO()
            {
                Id = p.Id,
                CreatedDateTime = p.DatumPrijave,
                RepairedDateTime = p.DatumPopravka,
                Subject = p.Naslov,
                Description = p.Opis,
                ReporterId = p.UserPrijavas.FirstOrDefault(p => !p.IsRepairer).UserId,
                ReporterFullName = p.UserPrijavas.FirstOrDefault(p => !p.IsRepairer).User.Name,
                RepairerId = p.UserPrijavas.FirstOrDefault(p => p.IsRepairer).UserId,
                RepairerFullName = p.UserPrijavas.FirstOrDefault(p => p.IsRepairer).User.Name,
                StatusId = p.StatusId,
                StatusName = p.Status.Name,
                PrioriterId = p.PrioritetId,
                PrioritetWeight = p.Prioritet.Weight
            }).ToList();


            return prijavaResponseDTO;
        }
        internal async Task<List<PrijavaResponseDTO>> GetAllPrijave()
        {
            var prijave = await _prijavaRepository.GetAllPrijave();

            var prijavaResponseDTO = prijave.Select(p => new PrijavaResponseDTO()
            {
                Id = p.Id,
                CreatedDateTime = p.DatumPrijave,
                RepairedDateTime = p.DatumPopravka,
                Subject = p.Naslov,
                Description = p.Opis,
                ReporterId = p.UserPrijavas.FirstOrDefault(p => !p.IsRepairer).UserId,
                ReporterFullName = p.UserPrijavas.FirstOrDefault(p => !p.IsRepairer).User.Name,
                RepairerId = p.UserPrijavas.FirstOrDefault(p => p.IsRepairer).UserId,
                RepairerFullName = p.UserPrijavas.FirstOrDefault(p => p.IsRepairer).User.Name,
                StatusId = p.StatusId,
                StatusName = p.Status.Name,
                PrioriterId = p.PrioritetId,
                PrioritetWeight = p.Prioritet.Weight
            }).ToList();


            return prijavaResponseDTO;
        }

        internal async Task<Prijava?> AddNewPrijave(PrijavaRequestDTO newPrijavaRequestDTO)
        {
            var prijava = new Prijava()
            {
                KategorijaId = newPrijavaRequestDTO.KategorijaId,
                DatumPrijave = newPrijavaRequestDTO.CreatedDateTime,
                Naslov = newPrijavaRequestDTO.Subject,
                Opis = newPrijavaRequestDTO.Description,
                PrioritetId = newPrijavaRequestDTO.PrioriterId,
                StatusId = newPrijavaRequestDTO.StatusId
            };

            Prijava? savedPrijava = await _prijavaRepository.CreatePrijava(prijava);
            return savedPrijava;

        }

        internal async Task GetAllUnsetPrijave()
        {
            throw new NotImplementedException();
        }
    }
}
