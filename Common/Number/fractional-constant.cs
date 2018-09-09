using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace X.IO.Common.Number
{


    /*
    fractional-constant:
        digit-sequence? "." digit-sequence
        | digit-sequence "."
    */

    public class fractional_constant
    {
        public int begin_index { get; }
        public int end_index { get; }
        public string expression { get; }
        public digit_sequence lhs_digit_sequence { get; set; }
        public digit_sequence rhs_digit_sequence { get; set; }
        public SpecialToken ident { get; set; }

        public fractional_constant(digit_sequence _rhs_digit_sequence, SpecialToken _ident, digit_sequence _lhs_digit_sequence = null)
        {
            lhs_digit_sequence = _lhs_digit_sequence;
            rhs_digit_sequence = _rhs_digit_sequence;
            ident = _ident;
            expression = lhs_digit_sequence?.expression + "." + rhs_digit_sequence?.expression;
            begin_index = lhs_digit_sequence == null ? rhs_digit_sequence.begin_index-1 : lhs_digit_sequence.begin_index; // -1 for "."
            end_index = rhs_digit_sequence == null ? lhs_digit_sequence.end_index : rhs_digit_sequence.end_index;
        }

        public fractional_constant(digit_sequence _lhs_digit_sequence, SpecialToken _ident)
        {
            lhs_digit_sequence = _lhs_digit_sequence;           
            ident = _ident;
            expression = lhs_digit_sequence?.expression + ".";
            begin_index =  lhs_digit_sequence.begin_index;
            end_index = lhs_digit_sequence.end_index;
        }

    }
}
