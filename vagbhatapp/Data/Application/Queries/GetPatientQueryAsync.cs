using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vagbhatapp.Data.Dtos;

namespace vagbhatapp.Data.Application.Queries
{
    public class GetPatientQueryAsync : IRequest<PatientDto>
    {
        public readonly string id;
        public GetPatientQueryAsync(string id)
        {
            this.id = id;
        }       
    }
}
