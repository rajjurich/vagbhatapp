using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Application.Queries;
using vagbhatapp.Data.Dtos;

namespace vagbhatapp.Pages
{
    public class PatientProfileBase: ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IMediator Mediator { get; set; }
        protected PatientProfileDto Patient { get; set; }
        [Parameter]
        public string Visible { get; set; } = "show";
        public string DivCssClass => Visible == "show" ? "collapse" : null;
        protected override async Task OnInitializedAsync()
        {
            var query = new GetPatientProfileQueryAsync(Id);
            Patient = await Mediator.Send(query);
        }
    }
}
