using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Store.PagePatient
{
    public class PagePatientReducer : Reducer<PagePatientState, PagePatientAction>
    {
        public override PagePatientState Reduce(PagePatientState state, PagePatientAction action)
        {
            return new PagePatientState(action.Page);
        }
    }
}
