using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Data.Entities
{
    public class Treatment
    {
        public string TreatmentId { get; set; }
        public string Complain { get; set; }
        public string RxTreatment { get; set; }
        public string Diagnosis { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}
