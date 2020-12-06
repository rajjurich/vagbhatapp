using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Store.SearchPatient
{
    public class SearchState
    {
        public string Search { get; }
        public SearchState(string search)
        {
            Search = search;
        }
    }
}
