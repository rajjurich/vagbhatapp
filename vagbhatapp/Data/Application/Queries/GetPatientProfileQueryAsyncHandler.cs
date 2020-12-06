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
    public class GetPatientProfileQueryAsyncHandler : IRequestHandler<GetPatientProfileQueryAsync,
        PatientProfileDto>
    {
        private readonly ICommonService service;
        private readonly IMapper mapper;

        public GetPatientProfileQueryAsyncHandler(ICommonService service
            , IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<PatientProfileDto> Handle(GetPatientProfileQueryAsync request,
            CancellationToken cancellationToken)
        {            
            var patientProfileDto = mapper.Map<PatientProfileDto>(await service
                .GetPatientProfile(request.id));
            return patientProfileDto;
        }
    }
}
