using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Gestion_Recommandation.Services;
using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Services.Exceptions;

namespace Gestion_Recommandation.Components
{
    public partial class RegisterForm
    {
        [Inject] public IAuthService AuthenticationService { get; set; }
        [Inject] public IAdminService AdminService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }


        private RegisterRequest _model = new();
        private bool _isBusy = false;
        private string _errorMessage = string.Empty;
        private IEnumerable<Service> _services;
        private Service _service;

        protected override async Task OnInitializedAsync()
        {
            _services = (await AdminService.GetAllServicesAsync()).Value;
        }

        private async Task RegisterUserAsync()
        {
            _isBusy = true;
            _errorMessage = string.Empty;

            try
            {
                await AuthenticationService.RegisterUserAsync(_model);
                Navigation.NavigateTo("/");
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
        private void RedirectToLogin() => Navigation.NavigateTo("/");


        // Customisation
        bool isShow;
        InputType PasswordInput = InputType.Password;
        string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        void ButtonTestclick()
        {
            if (isShow)
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

        bool isShowConfirm;
        InputType ConfirmPasswordInput = InputType.Password;
        string ConfirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
        void ButtonTestclickConfirm()
        {
            if (isShowConfirm)
            {
                isShowConfirm = false;
                ConfirmPasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                ConfirmPasswordInput = InputType.Password;
            }
            else
            {
                isShowConfirm = true;
                ConfirmPasswordInputIcon = Icons.Material.Filled.Visibility;
                ConfirmPasswordInput = InputType.Text;
            }
        }
    }
}