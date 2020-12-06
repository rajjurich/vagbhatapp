using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Services;

namespace vagbhatapp.Data.Application.Queries
{
    public class GetFeesQueryAsyncHandler : IRequestHandler<GetFeesQueryAsync, FeesDto>
    {
        private readonly ICommonService service;
        private readonly IMapper mapper;
        private readonly IAppointmentService appointmentService;

        public GetFeesQueryAsyncHandler(ICommonService service
            , IMapper mapper
            , IAppointmentService appointmentService)
        {
            this.service = service;
            this.mapper = mapper;
            this.appointmentService = appointmentService;
        }
        public async Task<FeesDto> Handle(GetFeesQueryAsync request, CancellationToken cancellationToken)
        {
            var patients = service.GetFees(request.feesRequest).ToList();

            var totalFees = appointmentService
                .Find(x => x.AppointmentDate >= request.feesRequest.FromDate && x
                .AppointmentDate <= request.feesRequest.ToDate)
                .Sum(x => x.Fees);

            var fees = new FeesDto
            {
                Patients = patients,
                TotalFees = totalFees
            };
            return await Task.FromResult(fees);
        }
    }
}
