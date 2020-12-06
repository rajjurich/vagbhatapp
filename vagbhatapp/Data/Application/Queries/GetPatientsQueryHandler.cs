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
    public class GetPatientsQueryHandler : RequestHandler<GetPatientsQuery, IQueryable<PatientDto>>
    {
        private readonly IPatientService service;
        private readonly IMapper mapper;

        public GetPatientsQueryHandler(IPatientService service
            , IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        protected override IQueryable<PatientDto> Handle(GetPatientsQuery request)
        {
            return mapper.Map<IList<PatientDto>>(service.Get().OrderBy(x => x.PatientName))
                .AsQueryable();
        }
    }
}
