using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCarManagement.Client.Pages
{
    public class IndexBase: ComponentBase
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected bool displayMap = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initialize", null);
            }
        }

        protected void OpenMap()
        {
            displayMap = !displayMap;
        }
    }
}
