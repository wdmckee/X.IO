using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
    public class factor_sequence
    {

        /*

      factor-sequence:
	infix-operator-type2 factor
	| infix-operator-type2 factor factor-sequence
       */
        public string expression { get; }
        public infix_operator_type2 infix_operator_type2 { get; set; }
        public factor factor { get; set; }


        public factor_sequence self_factor_sequence { get; set; }

        public factor_sequence(infix_operator_type2 _infix_operator_type2, factor _factor)
        {
            infix_operator_type2 = _infix_operator_type2;
            factor = _factor;
            expression = infix_operator_type2.expression + factor.expression ;
        }

        public factor_sequence(infix_operator_type2 _infix_operator_type2, factor _factor, factor_sequence _factor_sequence)
        {
            infix_operator_type2 = _infix_operator_type2;
            factor = _factor;
            self_factor_sequence = _factor_sequence;
            expression = self_factor_sequence?.expression + infix_operator_type2.expression + factor.expression ;
        }

    }
}
