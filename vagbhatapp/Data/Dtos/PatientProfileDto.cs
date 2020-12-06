using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Data.Dtos
{
    public class PatientProfileDto
    {
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientHistory { get; set; }
        public List<AppointmentProfileDto> Appointments { get; set; }
    }

    public class AppointmentProfileDto
    {
        public string AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public double Fees { get; set; }
        public string PatientId { get; set; }
        public TreatmentProfileDto Treatment { get; set; }
    }
    public class TreatmentProfileDto
    {
        public string TreatmentId { get; set; }
        public string Complain { get; set; }
        public string RxTreatment { get; set; }
        public string Diagnosis { get; set; }
    }
}
