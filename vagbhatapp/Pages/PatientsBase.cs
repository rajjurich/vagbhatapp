using Fluxor;
using Fluxor.Blazor.Web.Components;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using vagbhatapp.Data.Application.Queries;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Store.PagePatient;
using vagbhatapp.Store.SearchPatient;

namespace vagbhatapp.Pages
{
    public class PatientsBase : ComponentBase
    {
        [Inject]
        public IMediator Mediator { get; set; }
        protected IEnumerable<PatientDto> Patients { get; set; }
        [Inject]
        public IDispatcher Dispatcher { get; set; }
        [Inject]
        public IState<SearchState> SeachState { get; set; }
        [Inject]
        public IState<PagePatientState> PagePatientState { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public ElementReference searchPatientText;
        public string Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public int Start { get; set; }
        protected override async Task OnInitializedAsync()
        {
            PageNumber = PagePatientState.Value.Page;
            PageSize = 10;
            //Search = SeachState.Value.Search;
            await GetPatientBySearch(PagePatientState.Value.Page * PageSize);
            //if (Search != "")
            //{
            //    var res = await PatientClient.GetPatients(Search, PagePatientState.Value.Page * PageSize, PageSize);
            //    Patients = res.Data;
            //    TotalRecords = Convert.ToInt32(res.RecordsFiltered.ToString());
            //    TotalPages = (int)Math.Ceiling(TotalRecords / (decimal)PageSize);
            //}
            //else
            //{
            //    var res = await PatientClient.GetPatients(PagePatientState.Value.Page * PageSize, PageSize);
            //    Patients = res.Data;
            //    TotalRecords = Convert.ToInt32(res.RecordsTotal.ToString());
            //    TotalPages = (int)Math.Ceiling(TotalRecords / (decimal)PageSize);
            //}
        }
        protected async Task OnSearchTextChanged(ChangeEventArgs changeEventArgs)
        {
            string search = changeEventArgs.Value.ToString();
            Dispatcher.Dispatch(new SearchAction(search));
            PageNumber = 0;
            await GetPatientBySearch(0);
            //if (SeachState.Value.Search != "")
            //{
            //    var res = await PatientClient.GetPatients(SeachState.Value.Search, 0, PageSize);
            //    Patients = res.Data;
            //    TotalRecords = Convert.ToInt32(res.RecordsFiltered.ToString());
            //    TotalPages = (int)Math.Ceiling(TotalRecords / (decimal)PageSize);
            //}
            //else
            //{
            //    var res = await PatientClient.GetPatients(0, PageSize);
            //    Patients = res.Data;
            //    TotalRecords = Convert.ToInt32(res.RecordsTotal.ToString());
            //    TotalPages = (int)Math.Ceiling(TotalRecords / (decimal)PageSize);
            //}
        }
        protected async Task GetPatient(int start)
        {
            Dispatcher.Dispatch(new PagePatientAction(start));
            PageNumber = PagePatientState.Value.Page;
            //var res = await PatientClient.GetPatients(start * PageSize, PageSize);
            //Patients = res.Data;
            await GetPatientBySearch(start * PageSize);
        }

        private async Task GetPatientBySearch(int start)
        {
            if (SeachState.Value.Search != "")
            {
                var query = new GetPatientsSearchQueryAsync(SeachState.Value.Search);
                var result = await Mediator.Send(query);
                Patients = result.Skip(start).Take(PageSize);
                TotalRecords = Convert.ToInt32(result.Count().ToString());
                TotalPages = (int)Math.Ceiling(TotalRecords / (decimal)PageSize);
                Search = SeachState.Value.Search;
            }
            else
            {
                var query = new GetPatientsQuery();
                var result = await Mediator.Send(query);
                Patients = result.Skip(start).Take(PageSize);
                TotalRecords = Convert.ToInt32(result.Count().ToString());
                TotalPages = (int)Math.Ceiling(TotalRecords / (decimal)PageSize);
            }
        }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && SeachState.Value.Search != "")
            {
                await JSRuntime.InvokeVoidAsync("setFocusOnElement", searchPatientText);
            }
            //return base.OnAfterRenderAsync(firstRender);
        }
    }
}
