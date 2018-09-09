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

        public int begin_index { get; }
        public int end_index { get; }
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
            begin_index = (_sign == null ? _digit_sequence.begin_index : _sign.index) - 1;// this is terrible but we have to take the sign.index -1 to find where the "e" would be
            end_index = digit_sequence.end_index;
        }

    }
}
