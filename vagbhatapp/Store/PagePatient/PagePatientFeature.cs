using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vagbhatapp.Store.PagePatient
{
    public class PagePatientFeature : Feature<PagePatientState>
    {
        public override string GetName() => "PageNumber";

        protected override PagePatientState GetInitialState() => new PagePatientState(page: 0);
    }
}
