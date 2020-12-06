using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Entities;

namespace vagbhatapp.MappingProfile
{
    public class DtoToEntity : Profile
    {
        public DtoToEntity()
        {
            CreateMap<PatientDto, Patient>().ForMember(d => d.DateOfBirth,
                o => o.MapFrom(s => Convert.ToDateTime(DateTime.Now
                  .AddYears(-s.Age).AddDays(new Random().Next(-200, -30)).ToShortDateString())));
            CreateMap<PatientDto, Address>();
            CreateMap<PatientDto, Appointment>();
            CreateMap<PatientDto, Treatment>();
        }
    }
}
