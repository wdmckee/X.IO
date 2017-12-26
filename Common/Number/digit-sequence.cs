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

        public string expression { get; }
        public digit digit { get; set; }
        public digit_sequence self_digit_sequence { get; set; }

        public digit_sequence(digit _digit)
        {
            digit = _digit;
            expression = digit.expression;
        }

        public digit_sequence(digit _digit, digit_sequence _digit_sequence)
        {
            digit = _digit;
            self_digit_sequence = _digit_sequence;
            expression = self_digit_sequence?.expression + digit.expression;            
        }








    }
}
