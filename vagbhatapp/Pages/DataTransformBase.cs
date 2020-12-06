using DataTranform;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Core;
using vagbhatapp.Data.Entities;
using vagbhatapp.Data.Services;

namespace vagbhatapp.Pages
{
    public class DataTransformBase : ComponentBase
    {
        [Inject]
        public IPatientService PatientService { get; set; }
        [Inject]
        public IAddressService AddressService { get; set; }
        [Inject]
        public IAppointmentService AppointmentService { get; set; }
        [Inject]
        public ITreatmentService TreatmentService { get; set; }
        [Inject]
        public EntitiesContext EntitiesContext { get; set; }
        [Inject]
        public IConfiguration Configuration { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        protected override async Task OnInitializedAsync()
        {
            DataTransform();
            await JSRuntime.InvokeVoidAsync("alertMessage", "Success");
            //return base.OnInitializedAsync();
        }
        private void DataTransform()
        {
            GetData gd = new GetData();
            var dt = gd.GetPatientData();

            bool execute = Convert.ToBoolean(Configuration.GetSection("DTSExecute").Value);
            if (execute)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    var patient = new Patient
                    {
                        MobileNumber = dr["patient_contact"].ToString(),
                        DateOfBirth = Convert.ToDateTime(DateTime.Now
                         .AddYears(-Convert.ToInt32(dr["patient_age"].ToString()))
                         .AddDays(new Random().Next(-300, 300)).ToShortDateString()),
                        Gender = "m",
                        PatientHistory = dr["patient_history"].ToString(),
                        PatientName = dr["patient_name"].ToString()
                    };

                    Patient createdPatient = PatientService.AddAsync(patient).Result;

                    var address = new Address
                    {
                        PatientId = createdPatient.PatientId,
                        FullAddress = dr["patient_address"].ToString()
                    };

                    AddressService.AddAsync(address);

                    var aptdt = Convert.ToDateTime(Convert.ToDateTime(dr["patient_visit_date"].ToString())
                        .ToShortDateString());
                    var appointment = new Appointment
                    {
                        AppointmentDate = aptdt,
                        PatientId = createdPatient.PatientId,
                        IsVisited = true,
                        Fees = gd.GetFees(dr["patient_id"].ToString(), aptdt)
                    };

                    Appointment createdAppointment = AppointmentService.AddAsync(appointment).Result;

                    var treatment = new Treatment
                    {
                        Complain = dr["patient_present_complain"].ToString(),
                        RxTreatment = dr["rx_treatment"].ToString(),
                        TreatmentId = createdAppointment.AppointmentId
                    };

                    TreatmentService.AddAsync(treatment);

                    var dt1 = gd.GetPatientExData(dr["patient_id"].ToString());
                    foreach (DataRow dr1 in dt1.Rows)
                    {
                        aptdt = Convert.ToDateTime(Convert.ToDateTime(dr1["revisit_date"].ToString())
                            .ToShortDateString());
                        var st = Convert.ToBoolean(dr1["visit_status"].ToString());
                        if (st)
                        {
                            appointment = new Appointment
                            {
                                AppointmentDate = aptdt,
                                PatientId = createdPatient.PatientId,
                                IsVisited = st,
                                Fees = gd.GetFees(dr["patient_id"].ToString(), aptdt)
                            };

                            createdAppointment = AppointmentService.AddAsync(appointment).Result;

                            treatment = new Treatment
                            {
                                Complain = dr1["revisit_complain"].ToString(),
                                RxTreatment = dr1["revisit_rx_treatment"].ToString(),
                                TreatmentId = createdAppointment.AppointmentId
                            };

                            TreatmentService.AddAsync(treatment);
                        }
                        else
                        {
                            var newAppointment = new Appointment
                            {
                                AppointmentDate = aptdt,
                                PatientId = createdPatient.PatientId
                            };
                            AppointmentService.AddAsync(newAppointment);
                        }
                    }
                    EntitiesContext.SaveChangesAsync();
                }
            }
        }
    }
}
