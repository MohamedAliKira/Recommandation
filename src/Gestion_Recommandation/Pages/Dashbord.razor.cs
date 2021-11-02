using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Gestion_Recommandation;
using Gestion_Recommandation.Shared;
using MudBlazor;
using Gestion_Recommandation.Components;
using Gestion_Recommandation.Services;
using Gestion_Recommandation.Services.Exceptions;

namespace Gestion_Recommandation.Pages
{
    public partial class Dashbord
    {
        [Inject] public IRecommandationService RecommandationService { get; set; }
        private List<Shared.Models.Dashbord> ListDashbord = new();
        private bool _isBusy { get; set; }
        private string _errorMessage = string.Empty;

        public double[] data = Array.Empty<double>();
        public string[] labels = Array.Empty<string>();

        protected override async Task OnInitializedAsync()
        {
            ListDashbord = await GetDashbordAsync();
        }

        private async Task<List<Shared.Models.Dashbord>> GetDashbordAsync()
        {
            _isBusy = true;
            try
            {
                var result = await RecommandationService.DashbordAsync("");
                data = result.Value.Select(d => Convert.ToDouble(d.Count)).ToArray();
                labels = result.Value.Select(d => d.Bureau).ToArray();
                _isBusy = false;
                return result.Value.ToList();
            }
            catch (ApiException e)
            {
                _errorMessage = e.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
            _isBusy = false;
            return null;
        }
    }
}