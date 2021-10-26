using Gestion_Recommandation.Services.Exceptions;
using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Recommandation.Services
{
    public interface IAuthService
    {
        Task<ApiResponse> RegisterUserAsync(RegisterRequest model);
        Task<ApiResponse<LoginResult>> LoginUserAsync(LoginRequest model);
    }

    public class HttpAuthService : IAuthService
    {
        private readonly HttpClient _client;
        public HttpAuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse<LoginResult>> LoginUserAsync(LoginRequest model)
        {
            var response = await _client.PostAsJsonAsync("/api/auth/Login", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResult>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse> RegisterUserAsync(RegisterRequest model)
        {
            var response = await _client.PostAsJsonAsync("/api/auth/Register", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }
    }
}
