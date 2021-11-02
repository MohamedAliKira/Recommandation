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
using Blazored.LocalStorage;
using Gestion_Recommandation.Services;
using Gestion_Recommandation.Services.Exceptions;

namespace Gestion_Recommandation.Components
{
    public partial class LoginForm
    {
        [Inject] public IAuthService AuthService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] public ILocalStorageService Storage { get; set; }

        private LoginRequest _model = new LoginRequest();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;

        private async Task LoginUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                var response = await AuthService.LoginUserAsync(_model);
                if (response.IsSuccess)
                {                   
                    await Storage.SetItemAsStringAsync("access_token", response.Value.Token);
                    await Storage.SetItemAsync("expiry_date", response.Value.ExpiryDate);
                    await AuthenticationStateProvider.GetAuthenticationStateAsync();

                    Navigation.NavigateTo("/index");
                }
                else
                {
                    _errorMessage = response.Message;
                }
            }
            catch (ApiException ex)
            {
                // Handle the errors of API
                _errorMessage = ex.ApiErrorResponse.Message;
            }
            catch (Exception e)
            {
                // Handle the errors 
                _errorMessage = e.Message;
            }

            _isBusy = false;
        }

        private void RedirectToRegister() => Navigation.NavigateTo("/authentication/register");


        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        void ButtonTestclick()
        {
            if(isShow)
            {
                isShow = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                isShow = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}