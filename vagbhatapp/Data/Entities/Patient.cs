using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Data.Entities
{
    public class Patient
    {
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string PatientHistory { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
