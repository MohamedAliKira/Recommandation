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

namespace Gestion_Recommandation.Pages.Recommandations
{
    public partial class Recommands
    {
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] IDialogService DialogService { get; set; }

        private List<BreadcrumbItem> _breadcrumbItems = new()
        {
            new BreadcrumbItem("Acceuil", "/index"),
            new BreadcrumbItem("Recommndations", "/recommandations", true)
        };

        #region Edit
        private void EditRecoms(Shared.Models.Recommandations plan)
        {
            Navigation.NavigateTo($"/recommandation/form/{plan.Id}");
        }
        #endregion

        #region View
        private void ViewRecom(Shared.Models.Recommandations recom)
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

            DialogService.Show<Components.RecomDialog>($"Recommandation N°: {recom.NumeroReference} du {recom.DateReference.ToShortDateString()}", parameters, options);            
        }
        #endregion
    }
}