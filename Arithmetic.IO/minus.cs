
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class minus
    {

        public string expression { get; }
        public bool is_minus { get; }


        public minus(SpecialToken data)
        {
            // here we account for "-exp(7)" but despite being called "minus"
            // i guess it can account for "+exp(7)" as well though its kinda pointless.
            
            if (
                data.StringValue.Equals("-")
                | data.StringValue.Equals("+")
                )
            {
                is_minus = true;
                expression = data.StringValue;
            }
            else
            {
                is_minus = false;
            }
        }






    }
}
