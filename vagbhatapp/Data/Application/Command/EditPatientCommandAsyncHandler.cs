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
    public class EditPatientCommandAsyncHandler : IRequestHandler<EditPatientCommandAsync, PatientDto>
    {
        private readonly IPatientService patientService;
        private readonly IAddressService addressService;
        private readonly IAppointmentService appointmentService;
        private readonly ITreatmentService treatmentService;
        private readonly IMapper mapper;

        public EditPatientCommandAsyncHandler(IPatientService patientService
            , IAddressService addressService
            , IAppointmentService appointmentService
            , ITreatmentService treatmentService
            , IMapper mapper)
        {
            this.patientService = patientService;
            this.addressService = addressService;
            this.appointmentService = appointmentService;
            this.treatmentService = treatmentService;
            this.mapper = mapper;
        }
        public async Task<PatientDto> Handle(EditPatientCommandAsync request
            , CancellationToken cancellationToken)
        {
            return await EditPatient(request);
        }
        private async Task<PatientDto> EditPatient(EditPatientCommandAsync request)
        {
            int patientCheck = await patientService.CountAsync(x => x.PatientId == request.Dto.PatientId);
            if (patientCheck > 0)
            {
                var patient = mapper.Map<Patient>(request.Dto);               
                patient.Gender = "m";
                //patient.DateOfBirth = Convert.ToDateTime(DateTime.Now
                //      .AddYears(-request.Dto.Age).ToShortDateString());
                Patient createdPatient = await patientService.UpdateAsync(patient);


                var address = mapper.Map<Address>(request.Dto);

                await addressService.UpdateAsync(address);

                //var getappointment = appointmentService
                //    .Find(x => x.PatientId == patient.Id && x.IsVisited == false, 0, 1)
                //    .FirstOrDefault();

                var appointment = mapper.Map<Appointment>(request.Dto);

                //appointment.Id = getappointment.Id;
                appointment.AppointmentDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                appointment.IsVisited = true;
                appointment.PatientId = createdPatient.PatientId;
                Appointment createdAppointment = await appointmentService.UpdateAsync(appointment);


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
            return null;
        }
    }
}
