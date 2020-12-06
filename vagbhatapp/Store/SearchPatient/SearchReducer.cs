using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Store.SearchPatient
{
    public class SearchReducer : Reducer<SearchState, SearchAction>
    {
        public override SearchState Reduce(SearchState state, SearchAction action)
        {
            return new SearchState(action.Search);
        }
    }
}
