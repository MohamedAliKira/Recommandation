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

namespace Gestion_Recommandation.Components
{
    public partial class RecomForm
    {
        [Inject] public NavigationManager Navigation { get; set; }
        [Parameter] public string Id { get; set; }
    }
}