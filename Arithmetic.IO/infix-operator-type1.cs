
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class infix_operator_type1
    {


        public string expression { get; }
        public bool is_infix_operator_type1 { get; }


        public infix_operator_type1(SpecialToken data)
        {
            if (
                data.StringValue.Equals("+")
                | data.StringValue.Equals("-")
                )
            {
                is_infix_operator_type1 = true;
                expression = data.StringValue;
            }
            else
            {
                is_infix_operator_type1 = false;
            }
        }




    }
}
