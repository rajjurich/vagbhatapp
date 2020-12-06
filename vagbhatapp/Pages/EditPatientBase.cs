using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Application.Command;
using vagbhatapp.Data.Application.Queries;
using vagbhatapp.Data.Dtos;

namespace vagbhatapp.Pages
{
    public class EditPatientBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IMediator Mediator { get; set; }        
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public PatientDto Patient { get; set; } = new PatientDto();
        protected string VisibilityText { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new GetPatientQueryAsync(Id);
            Patient = await Mediator.Send(query);            
            VisibilityText = "show";
        }
        protected async Task SubmitForm()
        {
            var command = new EditPatientCommandAsync(Patient);
            var patient = await Mediator.Send(command);           
            if (patient != null)
            {
                await JSRuntime.InvokeVoidAsync("alertMessage", "Success");
            }
            NavigationManager.NavigateTo("/patients");
        }
        protected void ViewProfile()
        {
            VisibilityText = VisibilityText == "show" ? "hide" : "show";
        }
    }
}
