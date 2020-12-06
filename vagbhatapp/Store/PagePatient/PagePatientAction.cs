using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Store.PagePatient
{
    public class PagePatientAction
    {
        public int Page { get; }
        public PagePatientAction(int page)
        {
            Page = page;
        }
    }
}
