using Blazored.LocalStorage;
using Gestion_Recommandation.Services.Exceptions;
using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Gestion_Recommandation.Services
{
    public interface IAdminService
    {
        // ------------------------- USERS
        Task<ApiResponse<IEnumerable<UserIdentity>>> GetAllUsersAsync();
        Task<ApiResponse<UserIdentity>> EditUserAsync(UserIdentity user);
        Task<ApiResponse<UserIdentity>> DeleteUserAsync(string userId);

        // ------------------------- ROLES
        Task<ApiResponse<IEnumerable<IdentityRole>>> GetAllRolesAsync();
        Task<ApiResponse<IdentityRole>> CreateRoleAsync(string roleName);
        Task<ApiResponse<IdentityRole>> EditRoleAsync(IdentityRole role);
        Task<ApiResponse<IdentityRole>> DeleteRoleAsync(string roleId);

        //--------------------------- MANAGE_USER_ROLE_CLAIMS
        Task<ApiResponse> ManageUserRolesAsync(List<IdentityRole> listOfRoles, string userId);
        Task<ApiResponse> ManageUserClaimsAsync(List<UsersClaims> Claims, string userId);

        //-------------------------- SERVICES
        Task<ApiResponse<IEnumerable<Service>>> GetAllServicesAsync();
        Task<ApiResponse<Service>> CreateServiceAsync(string serviceName, int tutelle);
        Task<ApiResponse<Service>> EditServiceAsync(Service service);
    }

    public class HttpAdminService : IAdminService
    {
        private readonly HttpClient _client;
        private ILocalStorageService _localStorage;
        public HttpAdminService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public Task<ApiResponse<IdentityRole>> CreateRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Service>> CreateServiceAsync(string serviceName, int tutelle)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IdentityRole>> DeleteRoleAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserIdentity>> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IdentityRole>> EditRoleAsync(IdentityRole role)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<Service>> EditServiceAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<UserIdentity>> EditUserAsync(UserIdentity user)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<IEnumerable<IdentityRole>>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<IEnumerable<Service>>> GetAllServicesAsync()
        {
            string requestUri = "/api/Admin/AllServices";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);            

            var response = await _client.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<Service>>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public Task<ApiResponse<IEnumerable<UserIdentity>>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> ManageUserClaimsAsync(List<UsersClaims> Claims, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> ManageUserRolesAsync(List<IdentityRole> listOfRoles, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
