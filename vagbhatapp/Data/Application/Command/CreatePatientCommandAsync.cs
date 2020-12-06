using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Dtos;

namespace vagbhatapp.Data.Application.Command
{
    public class CreatePatientCommandAsync : IRequest<PatientDto>
    {
        public CreatePatientCommandAsync(PatientDto dto)
        {
            Dto = dto;
        }

        public PatientDto Dto { get; }
    }
}
