using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Store.SearchPatient
{
    public class SearchAction
    {
        public string Search { get; }
        public SearchAction(string search)
        {
            Search = search;
        }
    }
}
