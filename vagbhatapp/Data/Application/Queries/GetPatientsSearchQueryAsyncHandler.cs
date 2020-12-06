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
using vagbhatapp.Data.Application.Queries;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Services;

namespace vagbhatapp.Data.Application.Queries
{
    public class GetPatientsSearchQueryAsyncHandler : RequestHandler<GetPatientsSearchQueryAsync, IQueryable<PatientDto>>
    {
        private readonly IPatientService service;

        private readonly IMapper mapper;

        public GetPatientsSearchQueryAsyncHandler(IPatientService service
            , IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        protected override IQueryable<PatientDto> Handle(GetPatientsSearchQueryAsync request)
        {
            return mapper.Map<List<PatientDto>>(service
                 .Find(x => x.PatientName
                 .Contains(request.search) || x.MobileNumber
                 .Contains(request.search)))
                 .AsQueryable();
        }
    }
}
