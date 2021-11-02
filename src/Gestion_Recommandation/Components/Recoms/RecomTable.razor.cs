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
using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Services.Exceptions;
using Gestion_Recommandation.Services;

namespace Gestion_Recommandation.Components
{
    public partial class RecomTable
    {
        [Inject] public IRecommandationService RecommandationService { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationState { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Parameter] public EventCallback<Recommandations> OnEditClicked { get; set; }
        [Parameter] public EventCallback<Recommandations> OnViewClicked { get; set; }

        private string _query = string.Empty;
        private MudTable<Recommandations> _table;
        
        private string _errorMessage = string.Empty;

        private async Task<TableData<Recommandations>> ServerReloadAsync(TableState state)
        {            
            System.Threading.Thread.Sleep(3000);
            try
            {
                var user = (await AuthenticationState.GetAuthenticationStateAsync()).User;
                string userId = user.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;

                var result = await RecommandationService.GetRecommandationsAsync(_query, userId, state.Page, state.PageSize);
                return new TableData<Recommandations>
                {
                    Items = result.Value.Records,
                    TotalItems = result.Value.ItemsCount
                };
            }
            catch (ApiException e)
            {
                _errorMessage = e.ApiErrorResponse.Message;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }            
            return null;
        }
        private void OnSearch(string query)
        {
            _query = query;
            _table.ReloadServerData();
        }
        

        

    }
}