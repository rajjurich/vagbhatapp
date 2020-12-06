using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Entities;
using vagbhatapp.Data.Services;

namespace vagbhatapp.Data.Application.Command
{
    public class CreatePatientCommandAsyncHandler : IRequestHandler<CreatePatientCommandAsync, PatientDto>
    {
        private readonly IPatientService patientService;
        private readonly IAddressService addressService;
        private readonly IAppointmentService appointmentService;
        private readonly ITreatmentService treatmentService;
        private readonly IMapper mapper;
        //private readonly IHttpContextAccessor httpContextAccessor;
        //readonly string usrid = "11ed58b5-c1af-45e5-a0b2-2f5355d5d8d5";
        //readonly string docid = "00448D9A-AE9A-444D-968E-1DE50EF4BB2F";

        public CreatePatientCommandAsyncHandler(IPatientService patientService
            , IAddressService addressService
            , IAppointmentService appointmentService
            , ITreatmentService treatmentService
            , IMapper mapper
            //, IHttpContextAccessor httpContextAccessor
            )
        {
            this.patientService = patientService;
            this.addressService = addressService;
            this.appointmentService = appointmentService;
            this.treatmentService = treatmentService;
            this.mapper = mapper;
            //this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<PatientDto> Handle(CreatePatientCommandAsync request
            , CancellationToken cancellationToken)
        {
            return await CreatePatient(request);
        }

        private async Task<PatientDto> CreatePatient(CreatePatientCommandAsync request)
        {
            var patient = mapper.Map<Patient>(request.Dto);          
            patient.Gender = "m";
            //patient.DateOfBirth = Convert.ToDateTime(DateTime.Now
            //      .AddYears(-request.Dto.Age).ToShortDateString());
            Patient createdPatient = await patientService.AddAsync(patient);

            var address = mapper.Map<Address>(request.Dto);
            address.PatientId = createdPatient.PatientId;
            await addressService.AddAsync(address);

            var appointment = mapper.Map<Appointment>(request.Dto);
            appointment.AppointmentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            appointment.IsVisited = true;
            appointment.PatientId = createdPatient.PatientId;            
            Appointment createdAppointment = await appointmentService.AddAsync(appointment);

            var newAppointment = new Appointment
            {
                AppointmentDate = request.Dto.NextAppointmentDate,
                PatientId = createdPatient.PatientId
            };
            await appointmentService.AddAsync(newAppointment);

            var treatment = mapper.Map<Treatment>(request.Dto);
            treatment.TreatmentId = createdAppointment.AppointmentId;
            await treatmentService.AddAsync(treatment);

            return mapper.Map<PatientDto>(createdPatient);

            
        }
    }
}
