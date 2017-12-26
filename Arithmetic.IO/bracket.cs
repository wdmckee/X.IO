
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class bracket
    {



        public string expression { get; }
        public bool is_bracket { get; }


        public bracket(SpecialToken data)
        {
            if (
                data.StringValue.Equals("[")
                | data.StringValue.Equals("]")
                )
            {
                is_bracket = true;
                expression = data.StringValue;
            }
            else
            {
                is_bracket = false;
            }
        }





    }
}
