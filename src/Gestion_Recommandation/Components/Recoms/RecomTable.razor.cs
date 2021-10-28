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
using Gestion_Recommandation.Components.Recoms;

namespace Gestion_Recommandation.Components
{
    public partial class RecomTable
    {
        [Inject] public IRecommandationService RecommandationService { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationState { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        private string _query = string.Empty;
        private MudTable<Recommandations> _table;
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task<TableData<Recommandations>> ServerReloadAsync(TableState state)
        {
            _isBusy = true;
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
            _isBusy = false;
            return null;
        }
        private void OnSearch(string query)
        {
            _query = query;
            _table.ReloadServerData();
        }

        private void EditRecom(Recommandations recom)
        {
            Navigation.NavigateTo($"/recommandation/form/{recom.Id}");
        }

        private void ViewRecom(Recommandations recom)
        {
            var parameters = new DialogParameters
            {
                { "RecommandationDetail", recom }
            };
            var options = new DialogOptions() 
            { 
                CloseButton = true, 
                MaxWidth = MaxWidth.Small, 
                FullWidth = true 
            };

            DialogService.Show<RecomDialog>($"Recommandation N°: {recom.NumeroReference} du {recom.DateReference.ToShortDateString()}", parameters, options);
            _isBusy = false;
        }

    }
}