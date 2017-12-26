using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Number
{

    /* integer-constant:
                     digit-sequence */

    public class integer_constant
    {
        public string expression { get; }
        public digit_sequence digit_sequence { get; set; }

        public integer_constant(digit_sequence _digit_sequence)
        {
            digit_sequence = _digit_sequence;
            expression = digit_sequence.expression;
        }

    }
}
