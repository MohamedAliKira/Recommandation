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
        Task<ApiResponse<PagedList<Recommandations>>> GetRecommandationsAsync(string query = null, string userId = null, int pageNumber = 1, int pageSize = 12);
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
            recomm.Bureau = model.Bureau;

            // Insertion de la trace
            var trace = new Trace_Recommandations()
            {
                AgentSaisie = model.ID_User,
                AgentBureau = model.Bureau,    
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

        public async Task<ApiResponse<PagedList<Recommandations>>> GetRecommandationsAsync(string query = null, string userId = null, int pageNumber = 1, int pageSize = 12)
        {
            if (string.IsNullOrWhiteSpace(query))
                query = "";
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageSize < 5)
                pageSize = 5;
            if (pageSize > 50)
                pageSize = 50;
            
            var recommds = await (from p in _context.Recommandations
                                  where p.ID_User == userId &&
                                 (p.NumeroReference.Contains(query) ||
                                  p.DeLaPart.Contains(query) ||
                                  p.Source.Contains(query) ||
                                  p.InstructionDRH.Contains(query) ||
                                  p.Type.Contains(query))
                                  orderby p.Id descending
                                  select p).ToArrayAsync();
            var rslt = new PagedList<Recommandations>(recommds, pageNumber, pageSize);
            return new ApiResponse<PagedList<Recommandations>>
            {
                IsSuccess = true,
                Message = "Gets succesfully !",
                Value = rslt
            };
        }
    }
}
