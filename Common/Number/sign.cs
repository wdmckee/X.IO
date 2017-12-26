using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace X.IO.Common.Number
{
    public class sign
    {
        public string expression { get; set; }// only place we allow a SET on an expression
        public bool is_sign { get; }
        public SpecialToken token { get; set; }
        public sign(SpecialToken data)
        {
            if (data.StringValue == "-" || data.StringValue == "+")
            {
                token = data;
                is_sign = true;
                expression = data.StringValue;
            }
            else
            {
                is_sign = false;
            }
        }


        // THIS SECTION IS KIND OF CHEATING. I SHOULD DO THIS IN A BNF SEQUENCE.
        // GO BACK AND REDO THIS !!!!
        public sign(List<sign> _sign_sequnce)
        {
            sign current = null;
            sign prev = null;
            string _expression = string.Empty;

            #region loop
            foreach (var _sign in _sign_sequnce)
            {

                /*
                    -- = +
                    ++ = +
                    -+ = -
                    +- = -   
                */
                
                current = _sign;
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
                        current = new sign(new SpecialToken('+'));
                    }
                    else
                    {
                        current = new sign(new SpecialToken('-'));
                    }
                    
                }

                prev = current;


            }
            #endregion

            if (_sign_sequnce.Count > 0)
            { 
                token = current.token;
                is_sign = true;
                expression = _expression;
            }
            else
            {
                is_sign = false;
            }
            }
    }
}
