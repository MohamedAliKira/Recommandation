using Blazored.LocalStorage;
using Gestion_Recommandation.Services.Exceptions;
using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Shared.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Recommandation.Services
{
    public interface IRecommandationService
    {
        Task<ApiResponse<PagedList<Recommandations>>> GetRecommandationsAsync(string query = null, string userId = null, int pageNumber = 1, int pageSize = 12);
        Task<ApiResponse<Recommandations>> GetByIdAsync(int Id, string userId);
        Task<ApiResponse<Recommandations>> CreateAsync(Recommandations model);
        Task<ApiResponse<Recommandations>> EditAsync(Recommandations model);
        Task<ApiResponse<IEnumerable<Dashbord>>> DashbordAsync(string bureau);
    }
    public class HttpRecommandationService : IRecommandationService
    {
        private readonly HttpClient _client;
        private ILocalStorageService _localStorage;
        public HttpRecommandationService(HttpClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<ApiResponse<Recommandations>> CreateAsync(Recommandations model)
        {
            string requestUri = "/api/Recommandations";
            string serializedObject = JsonConvert.SerializeObject(model);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri);

            var token = await _localStorage.GetItemAsync<string>("access_token");
            requestMessage.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedObject);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _client.SendAsync(requestMessage);


            //var response = await _client.PostAsJsonAsync("/api/Recommandations", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<Recommandations>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<IEnumerable<Dashbord>>> DashbordAsync(string bureau)
        {
            string requestUri = $"/api/Recommandations/Dashbord?Bureau={bureau}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await _localStorage.GetItemAsync<string>("access_token");
            requestMessage.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<IEnumerable<Dashbord>>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<Recommandations>> EditAsync(Recommandations model)
        {
            string serializedObject = JsonConvert.SerializeObject(model);
            string requestUri = "/api/Recommandations";
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri);

            var token = await _localStorage.GetItemAsync<string>("access_token");
            requestMessage.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            requestMessage.Content = new StringContent(serializedObject);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await _client.SendAsync(requestMessage);

            //var response = await _client.PutAsJsonAsync("/api/Recommandations", model);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<Recommandations>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<Recommandations>> GetByIdAsync(int Id, string userId)
        {
            string requestUri = $"/api/Recommandations?id={Id}&userId={userId}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await _localStorage.GetItemAsync<string>("access_token");
            requestMessage.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<Recommandations>>();
                return result;
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<ApiErrorResponse>();
                throw new ApiException(errorResponse, response.StatusCode);
            }
        }

        public async Task<ApiResponse<PagedList<Recommandations>>> GetRecommandationsAsync(string query = null, string userId = null, int pageNumber = 1, int pageSize = 12)
        {
            string requestUri = $"/api/Recommandations/AllRecommandations?query={query}&userId={userId}&pageNumber={pageNumber}&pageSize={pageSize}";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var token = await _localStorage.GetItemAsync<string>("access_token");
            requestMessage.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<PagedList<Recommandations>>>();
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
