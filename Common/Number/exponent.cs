using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace X.IO.Common.Number
{

    /*
    exponent:
    ( "e" | "E" ) sign? digit-sequence
    */

    public class exponent
    {


        public string expression { get; }
        public sign sign { get; set; }
        public digit_sequence digit_sequence { get; set; }
        public SpecialToken ident { get; set; }

        public exponent(SpecialToken _ident, digit_sequence _digit_sequence, sign _sign = null)
        {
            ident = _ident;
            digit_sequence = _digit_sequence;
            sign = _sign;
            expression = "E" + sign?.expression + digit_sequence.expression;
        }

    }
}
