using MediatR;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Application.Queries;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Request;

namespace vagbhatapp.Pages
{
    public class FeesBase : ComponentBase
    {
        public FeesRequest FeesRequest { get; set; } = new FeesRequest();
        [Inject]
        public IMediator Mediator { get; set; }
        protected IEnumerable<PatientDto> Patients { get; set; }
        public string TotalFees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetFeesDetails();
            //return base.OnInitializedAsync();
        }
        protected async Task SubmitForm()
        {
            await GetFeesDetails();
        }

        private async Task GetFeesDetails()
        {
            var query = new GetFeesQueryAsync(FeesRequest);
            var result = await Mediator.Send(query);
            Patients = result.Patients;
            TotalFees = result.TotalFees.ToString();
        }
    }
}
