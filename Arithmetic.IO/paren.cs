
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class paren
    {



        public string expression { get; }
        public bool is_paren { get; }


        public paren(SpecialToken data)
        {
            if (
                data.StringValue.Equals("(")
                | data.StringValue.Equals(")")
                )
            {
                is_paren = true;
                expression = data.StringValue;
            }
            else
            {
                is_paren = false;
            }
        }





    }
}
