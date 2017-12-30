
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
        public string functional_expression { get; }
        public bool is_minus { get; }
        public SpecialToken token { get; set; }

        public minus(SpecialToken data)
        {
            // here we account for "-exp(7)" but despite being called "minus"
            // i guess it can account for "+exp(7)" as well though its kinda pointless.
            
            if (
                data.StringValue.Equals("-")
                | data.StringValue.Equals("+")
                )
            {
                token = data;
                is_minus = true;
                expression = data.StringValue;
            }
            else
            {
                is_minus = false;
            }
        }

        public minus(List<minus> _minus_sequnce)
        {
            minus current = null;
            minus prev = null;
            string _expression = string.Empty;

            #region loop
            foreach (var _minus in _minus_sequnce)
            {

                /*
                    -- = +
                    ++ = +
                    -+ = -
                    +- = -   
                */

                current = _minus;
                _expression += current.expression;

                if (prev != null)
                {

                    if (  // do we have a --?                      
                            current.token.StringValue == "-" && prev.token.StringValue == "-"
                         || prev.token.StringValue == "-" && current.token.StringValue == "-"
                         // or do we have a ++?
                         || (current.token.StringValue == "+" && prev.token.StringValue == "+")
                         || (prev.token.StringValue == "+" && current.token.StringValue == "+")
                        )
                    {
                        current = new minus(new SpecialToken('+'));
                    }
                    else
                    {
                        current = new minus(new SpecialToken('-'));
                    }

                }

                prev = current;


            }
            #endregion

            if (_minus_sequnce.Count > 0)
            {
                token = current.token;
                is_minus = true;
                expression = _expression;
                functional_expression = current.expression;
            }
            else
            {
                is_minus = false;
            }
        }




    }
}
