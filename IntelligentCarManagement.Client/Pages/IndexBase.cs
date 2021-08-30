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

        protected async Task OpenMap()
        {
            displayMap = !displayMap;
            if (displayMap)
            {
                await JSRuntime.InvokeVoidAsync("initialize", null);
            }
        }
    }
}
