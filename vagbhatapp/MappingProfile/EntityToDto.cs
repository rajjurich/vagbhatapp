using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Entities;

namespace vagbhatapp.MappingProfile
{
    public class EntityToDto : Profile
    {
        public EntityToDto()
        {


            CreateMap<Patient, PatientDto>().ForMember(d => d.Age,
                opt => opt.MapFrom(s => DateTime.Now.Year - s.DateOfBirth.Year));
            CreateMap<Patient, PatientProfileDto>()
                .ForMember(d => d.Appointments, o => o
                 .MapFrom(s => s.Appointments));
            CreateMap<Appointment, AppointmentProfileDto>();
            CreateMap<Treatment, TreatmentProfileDto>();
        }
    }
}
