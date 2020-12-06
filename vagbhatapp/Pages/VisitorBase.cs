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
    public class VisitorBase : ComponentBase
    {
        [Parameter]
        public string Visible { get; set; } = "show";
        public string DivCssClass => Visible == "show" ? "collapse" : null;
        [Inject]
        public IMediator Mediator { get; set; }
        protected IEnumerable<PatientDto> Patients { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new GetVisitorsQuery();
            Patients = await Mediator.Send(query);
        }
    }
}
