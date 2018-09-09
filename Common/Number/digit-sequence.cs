using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Number
{
    public class digit_sequence
    {
        /*  digit-sequence:
                     digit
                   | digit digit-sequence  */

        public int begin_index { get; }
        public int end_index { get; }
        public string expression { get; }
        public digit digit { get; set; }
        public digit_sequence self_digit_sequence { get; set; }

        public digit_sequence(digit _digit)
        {
            digit = _digit;
            expression = digit.expression;
            begin_index = digit.index;
            end_index = digit.index;
        }

        public digit_sequence(digit _digit, digit_sequence _digit_sequence)
        {
            digit = _digit;
            self_digit_sequence = _digit_sequence;
            expression = self_digit_sequence?.expression + digit.expression;
            begin_index = _digit_sequence == null ? digit.index : _digit_sequence.begin_index;
            end_index = digit.index;
        }








    }
}
