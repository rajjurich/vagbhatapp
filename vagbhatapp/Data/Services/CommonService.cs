using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vagbhatapp.Data.Core;
using vagbhatapp.Data.Dtos;
using vagbhatapp.Data.Entities;
using vagbhatapp.Data.Request;

namespace vagbhatapp.Data.Services
{
    public interface ICommonService
    {
        Task<Patient> GetPatientProfile(string id);
        IList<PatientDto> GetFees(FeesRequest feesRequest);
    }
    public class CommonService : ICommonService
    {
        private readonly EntitiesContext entitiesContext;

        public CommonService(EntitiesContext entitiesContext)
        {
            this.entitiesContext = entitiesContext;
        }

        public IList<PatientDto> GetFees(FeesRequest feesRequest)
        {
            var patients = entitiesContext.Appointments
               .Include(a => a.Patient)
               .Where(p => p.AppointmentDate >= feesRequest.FromDate &&
               p.AppointmentDate <= feesRequest.ToDate &&
               p.Fees > 0)
               .Select(p => new PatientDto
               {
                   PatientId = p.PatientId,
                   PatientName = p.Patient.PatientName,
                   NextAppointmentDate = p.AppointmentDate,
                   Fees = p.Fees
               })
               .OrderBy(x => x.PatientName)
               .ThenBy(x => x.NextAppointmentDate)
               .ToList();

            return patients;
        }

        public async Task<Patient> GetPatientProfile(string id)
        {

            var patient = await entitiesContext.Patients.Where(p => p.PatientId == id)
                .Include(a => a.Appointments)
                .ThenInclude(t => t.Treatment)
                .Where(p => p.Appointments.Any(a => a.IsVisited == true))
                .FirstOrDefaultAsync();

            patient.Appointments = patient.Appointments.OrderByDescending(c => c.AppointmentDate).ToList();

            return patient;

        }
    }
}
