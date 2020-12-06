using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Store.PagePatient
{
    public class PagePatientState
    {
        public int Page { get; }
        public PagePatientState(int page)
        {
            Page = page;
        }
    }
}
