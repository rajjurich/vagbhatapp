using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vagbhatapp.Data.Dtos;

namespace vagbhatapp.Data.Application.Queries
{
    public class GetPatientsQuery : IRequest<IQueryable<PatientDto>>
    {
        public GetPatientsQuery()
        {          
        }       
    }
}
