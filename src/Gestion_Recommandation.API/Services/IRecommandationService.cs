using Gestion_Recommandation.API.Models;
using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Shared.Response;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion_Recommandation.API.Services
{
    public interface IRecommandationService
    {
        Task<ApiResponse<List<Recommandations>>> GetRecommandationsAsync(string query = null, string userId = null);
        Task<ApiResponse<Recommandations>> GetByIdAsync(int Id, string userId);
        Task<ApiResponse<Recommandations>> CreateAsync(Recommandations model);
        Task<ApiResponse<Recommandations>> EditAsync(Recommandations model);
    }

    public class RecommandationService : IRecommandationService
    {
        private readonly AppDbContext _context;
        public RecommandationService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ApiResponse<Recommandations>> CreateAsync(Recommandations model)
        {
            if (model == null)
                return new ApiResponse<Recommandations>
                {
                    IsSuccess = false,
                    Message = "model is null",
                    Value = null
                };

            await _context.Recommandations.AddAsync(model);
            await _context.SaveChangesAsync();

            return new ApiResponse<Recommandations>
            {
                IsSuccess = true,
                Message = "création faite avec success !",
                Value = model
            };
        }

        public async Task<ApiResponse<Recommandations>> EditAsync(Recommandations model)
        {
            var recomm = await _context.Recommandations.FindAsync(model.Id);
            if (recomm == null)
                return new ApiResponse<Recommandations>
                {
                    IsSuccess = false,
                    Message = $"Recommandation with the Id: {model.Id} not found",
                    Value = null
                };

            recomm.NumeroReference = model.NumeroReference;
            recomm.DateReference = model.DateReference;
            recomm.DeLaPart = model.DeLaPart;
            recomm.Source = model.Source;
            recomm.IdentityRecommandation = model.IdentityRecommandation;
            recomm.InstructionDRH = model.InstructionDRH;
            recomm.Type = model.Type;

            // Insertion de la trace
            var trace = new Trace_Recommandations()
            {
                AgentSaisie = model.ID_User,
                DateSaisie = System.DateTime.Now,
                ID_Recommandations = model.Id,
                Description = "IdentityRecommandation:" + model.IdentityRecommandation + " | DeLaPart:" + model.DeLaPart + " | InstructionDRH:" + model.InstructionDRH
            };

            await _context.Trace_Recommandations.AddAsync(trace);
            await _context.SaveChangesAsync();

            return new ApiResponse<Recommandations>
            {
                IsSuccess = true,
                Message = "Modification faite avec success !",
                Value = model
            };
        }

        public async Task<ApiResponse<Recommandations>> GetByIdAsync(int Id, string userId)
        {
            var recom = await _context.Recommandations.FindAsync(Id);
            if (recom == null || recom.ID_User != userId)
                return new ApiResponse<Recommandations>
                {
                    IsSuccess = false,
                    Message = $"Recommandation with the Id: {Id} not found",
                    Value = null
                };

            return new ApiResponse<Recommandations>
            {
                IsSuccess = true,
                Message = "Recommandation trouvée !",
                Value = recom
            };
        }

        public async Task<ApiResponse<List<Recommandations>>> GetRecommandationsAsync(string query = null, string userId = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                query = "";

            var list = await _context.Recommandations.ToListAsync();

            var recommds = (from p in list
                            where p.ID_User == userId &&
                                 (p.NumeroReference.Contains(query) ||
                                  p.DeLaPart.Contains(query) ||
                                  p.Source.Contains(query) ||
                                  p.InstructionDRH.Contains(query) ||
                                  p.Type.Contains(query) ||
                                  p.DateReference.ToShortDateString().Contains(query))
                            orderby p.Id descending
                            select p).ToList();
            return new ApiResponse<List<Recommandations>>
            {
                IsSuccess = true,
                Message = $"Nombre d'enregistrement : {recommds.Count}",
                Value = recommds
            };
        }
    }
}
