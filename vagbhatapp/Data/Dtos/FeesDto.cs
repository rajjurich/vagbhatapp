using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Data.Dtos
{
    public class FeesDto
    {
        public List<PatientDto> Patients { get; set; }
        public double TotalFees { get; set; }
    }
}
