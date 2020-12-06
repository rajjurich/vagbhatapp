using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Data.Entities
{
    public class Address
    {
        public string AddressId { get; set; }
        public string FullAddress { get; set; }
        public string PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
