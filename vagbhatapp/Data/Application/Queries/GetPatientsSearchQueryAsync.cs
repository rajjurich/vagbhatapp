using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vagbhatapp.Data.Dtos;

namespace vagbhatapp.Data.Application.Queries
{
    public class GetPatientsSearchQueryAsync : IRequest<IQueryable<PatientDto>>
    {
        public readonly string search;

        public GetPatientsSearchQueryAsync(string search)
        {
            this.search = search;
        }
    }
}
