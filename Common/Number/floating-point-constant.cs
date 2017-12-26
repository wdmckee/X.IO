using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Number
{
    /*
    floating-point-constant:
        fractional-constant exponent?
        | digit-sequence exponent
    */

    public class floating_point_constant
    {
        public string expression { get; }
        public fractional_constant fractional_constant { get; set; }
        public exponent exponent { get; set; }
        public digit_sequence digit_sequence { get; set; }


        public floating_point_constant(fractional_constant _fractional_constant, exponent _exponent = null)
        {
            fractional_constant = _fractional_constant;
            exponent = _exponent;
            expression = fractional_constant.expression + exponent?.expression;

        }

        public floating_point_constant(digit_sequence _digit_sequence, exponent _exponent)
        {
            digit_sequence = _digit_sequence;
            exponent = _exponent;
            expression =  digit_sequence.expression + exponent?.expression;

        }


    }
}
