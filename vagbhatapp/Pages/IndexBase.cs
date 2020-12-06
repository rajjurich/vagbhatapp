using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Pages
{
    public class IndexBase : ComponentBase
    {
        protected string VisibilityText { get; set; }
        protected override Task OnInitializedAsync()
        {
            return Task.FromResult(VisibilityText = "show");
        }

        protected void ToggleVisibility()
        {
            VisibilityText = VisibilityText == "show" ? "hide" : "show";
        }
    }
}
