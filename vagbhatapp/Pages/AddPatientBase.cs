using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Application.Command;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Store.SearchPatient;

namespace vagbhatapp.Pages
{
    public class AddPatientBase : ComponentBase
    {
        [Inject]
        public IMediator Mediator { get; set; }
        public PatientDto Patient { get; set; } = new PatientDto();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public IState<SearchState> SeachState { get; set; }
        protected async Task SubmitForm()
        {           

            var command = new CreatePatientCommandAsync(Patient);

            var patient = await Mediator.Send(command);
            
            if (patient != null)
            {
                await JSRuntime.InvokeVoidAsync("alertMessage", "Success");
                Dispatcher.Dispatch(new SearchAction(patient.PatientName));
                NavigationManager.NavigateTo("/patients");
            }
        }
    }
}
