using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Utility
{
    public class comma_wsp
    {

        public int begin_index { get; }
        public int end_index { get; }
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
            begin_index = lhs_wsp.Count > 0 ? lhs_wsp.Min(x => x.index) : _comma.index;
            end_index = (_comma == null ? lhs_wsp.Max(x => x.index) : _comma.index)+__rhs_wsp.Count();   //note the .count on this one only
        }

        public comma_wsp(List<wsp> __lhs_wsp, comma _comma)
        {
            lhs_wsp = __lhs_wsp;
            comma = _comma;
            expression = new String(' ', lhs_wsp.Count()) + comma?.expression;
            begin_index = lhs_wsp.Count > 0 ?  lhs_wsp.Min( x => x.index ) : _comma.index  ;
            end_index = _comma == null ? lhs_wsp.Max(x => x.index) : _comma.index; 
        }

    }
}

/*
comma-wsp:
    (wsp+ comma? wsp*) | (comma wsp*)
*/
