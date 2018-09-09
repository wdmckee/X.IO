using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace X.IO.Common.Number
{
    public class sign
    {
        public int index { get; }
        public string expression { get; set; }// only place we allow a SET on an expression
        public bool is_sign { get; }
        public SpecialToken token { get; set; }
        public sign(SpecialToken data, int _index)
        {
            if (data.StringValue == "-" || data.StringValue == "+")
            {
                token = data;
                is_sign = true;
                expression = data.StringValue;
                index = _index;
            }
            else
            {
                is_sign = false;
            }
        }


        // THIS SECTION IS KIND OF CHEATING. I SHOULD DO THIS IN A BNF SEQUENCE.
        // GO BACK AND REDO THIS !!!! - INDEX GETS AFFECTED BY THIS WEIRDITY AS WELL
        public sign(List<sign> _sign_sequnce, int _index)
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
                        current = new sign(new SpecialToken('+'), index);
                    }
                    else
                    {
                        current = new sign(new SpecialToken('-'), index);
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
