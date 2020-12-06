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
    public class GetVisitorsQueryHandler : RequestHandler<GetVisitorsQuery, IQueryable<PatientDto>>
    {
        private readonly IPatientService service;
        private readonly IMapper mapper;

        public GetVisitorsQueryHandler(IPatientService service
            , IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        protected override IQueryable<PatientDto> Handle(GetVisitorsQuery request)
        {
            var dt = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            return mapper.Map<List<PatientDto>>(service
                .Find(x => x.Appointments
                .Any(x => x.AppointmentDate == dt && x.IsVisited == false)))
                .AsQueryable();
        }
    }
}
