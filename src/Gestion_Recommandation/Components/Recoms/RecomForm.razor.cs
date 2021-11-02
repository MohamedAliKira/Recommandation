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
using Gestion_Recommandation.Services.Exceptions;
using Gestion_Recommandation.Services;
using Gestion_Recommandation.Shared.Models;

namespace Gestion_Recommandation.Components
{
    public partial class RecomForm
    {
        [Inject] public IRecommandationService RecommandationService { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationState { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Parameter] public int Id { get; set; }

        private bool _isEditMode => Id != 0;
        private Recommandations _model = new();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            if (_isEditMode)
                await FetchPlanByIdAsync();
        }

        private async Task SubmitFormAsync()
        {
            _isBusy = true;
            try
            {
                var user = (await AuthenticationState.GetAuthenticationStateAsync()).User;
                string userId = user.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;
                string office = user.Claims.Where(c => c.Type == "Bureau").Select(c => c.Value).SingleOrDefault();

                _model.ID_User = userId;
                _model.Bureau = office;

                var result =(!_isEditMode) ? await RecommandationService.CreateAsync(_model) : await RecommandationService.EditAsync(_model);
                if (result.IsSuccess)
                {
                    Navigation.NavigateTo("/Recommandations");
                }
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
        }

        private async Task FetchPlanByIdAsync()
        {
            _isBusy = true;
            try
            {
                var user = (await AuthenticationState.GetAuthenticationStateAsync()).User;
                string userId = user.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;

                var result = await RecommandationService.GetByIdAsync(Id, userId);
                _model = result.Value;
            }
            catch (ApiException ex)
            {
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception e)
            {
                //TODO : Log the error
                _errorMessage = e.Message;
            }
            _isBusy = false;
        }
    }
}