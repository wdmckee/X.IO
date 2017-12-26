using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Utility
{
    public class comma_wsp
    {
        public string expression { get; }
        public List<wsp> lhs_wsp { get; set; } = new List<wsp>();
        public comma comma { get; set; }
        public List<wsp> rhs_wsp { get; set; } = new List<wsp>();



        public comma_wsp(List<wsp> __lhs_wsp, List<wsp> __rhs_wsp, comma _comma = null)
        {
            lhs_wsp = __lhs_wsp;
            comma = _comma;
            rhs_wsp = __rhs_wsp;
            expression = new String(' ', lhs_wsp.Count()) + comma?.expression + (rhs_wsp == null?"":new String(' ', rhs_wsp.Count()));
        }

        public comma_wsp(List<wsp> __lhs_wsp, comma _comma)
        {
            lhs_wsp = __lhs_wsp;
            comma = _comma;
            expression = new String(' ', lhs_wsp.Count()) + comma?.expression;
        }

    }
}

/*
comma-wsp:
    (wsp+ comma? wsp*) | (comma wsp*)
*/
