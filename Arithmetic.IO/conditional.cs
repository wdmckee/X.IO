using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class conditional
    {

        public string expression { get; }
        public bool is_conditional { get;  }
        public SpecialToken token { get; set; }
        public conditional(SpecialToken data)
        {
            if (data.StringValue.Equals("<") || data.StringValue.Equals(">") || data.StringValue.Equals("="))
            {
                is_conditional = true;
                token = data;
                expression = data.StringValue;
            }
            else
            {
                is_conditional = false;
            }
        }



    }
}
