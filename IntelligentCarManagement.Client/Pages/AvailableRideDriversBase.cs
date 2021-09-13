using IntelligentCarManagement.Models;
using Majorsoft.Blazor.Components.Common.JsInterop.Scroll;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace IntelligentCarManagement.Client.Pages
{
    public class AvailableRideDriversBase: ComponentBase
    {
        [Parameter] public Ride Ride { get; set; } = new();

        [Inject] public IJSRuntime JSRuntime { get; set; }

        protected List<BreadcrumbItem> _items = new List<BreadcrumbItem>
        {
            new BreadcrumbItem("Home", href: "#", icon: Icons.Material.Filled.Home),
            new BreadcrumbItem("Videos", href: "#", icon: Icons.Material.Filled.VideoLibrary),
            new BreadcrumbItem("Create", href: null, disabled: true, icon: Icons.Material.Filled.Create)
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initialize", null);
            }
        }

        protected async Task HandleSubmitAsync()
        {
            await JSRuntime.InvokeVoidAsync("scrollToDrivers", null);
        }
    }
}
