using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Services;

namespace vagbhatapp.Data.Application.Queries
{
    public class GetPatientQueryAsyncHandler : IRequestHandler<GetPatientQueryAsync, PatientDto>
    {
        private readonly IPatientService service;
        private readonly IAddressService addressService;
        private readonly IAppointmentService appointmentService;
        private readonly IMapper mapper;

        public GetPatientQueryAsyncHandler(IPatientService service
            , IAddressService addressService
            , IAppointmentService appointmentService
            , IMapper mapper)
        {
            this.service = service;
            this.addressService = addressService;
            this.appointmentService = appointmentService;
            this.mapper = mapper;
        }

        public async Task<PatientDto> Handle(GetPatientQueryAsync request,
            CancellationToken cancellationToken)
        {
            var patientDto = mapper.Map<PatientDto>(await service.GetAsync(request.id));

            var address = addressService.Find(x => x.PatientId == request.id).FirstOrDefault();
            patientDto.FullAddress = address.FullAddress;
            patientDto.AddressId = address.AddressId;

            var appointment = appointmentService
                .Find(x => x.PatientId == request.id && x.IsVisited == false)
                .FirstOrDefault();
            patientDto.AppointmentId = appointment.AppointmentId;
            return patientDto;
        }
    }
}
