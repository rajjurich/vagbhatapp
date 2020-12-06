using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Store.SearchPatient
{
    public class SearchFeature : Feature<SearchState>
    {
        public override string GetName() => "Search";

        protected override SearchState GetInitialState() => new SearchState(search: "");
    }
}
