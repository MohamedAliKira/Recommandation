using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Shared.Response;
using System.Threading.Tasks;

namespace Gestion_Recommandation.API.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResult>> LoginAsync(LoginRequest loginRequest);
        Task<ApiResponse> RegisterAsync(RegisterRequest registerRequest);
    }

    public class AuthService : IAuthService
    {
        public Task<ApiResponse<LoginResult>> LoginAsync(LoginRequest loginRequest)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
