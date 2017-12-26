using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common.Utility;

namespace X.IO.Arithmetic
{
    public class parameter_sequence
    {

        public string expression { get; }
        public comma comma { get; set; }
        public arith_expression_sequence arith_expression_sequence { get; set; }
        public parameter_sequence self_parameter_sequence { get; set; }



        public parameter_sequence(arith_expression_sequence _arith_expression_sequence)
        {
            arith_expression_sequence = _arith_expression_sequence;
            expression = arith_expression_sequence.expression;
        }

        public parameter_sequence(arith_expression_sequence _arith_expression_sequence, parameter_sequence _self_parameter_sequence, comma _comma)
        {
            comma = _comma;
            arith_expression_sequence = _arith_expression_sequence;
            self_parameter_sequence = _self_parameter_sequence;
            expression = self_parameter_sequence?.expression + comma?.expression + arith_expression_sequence.expression
          /* expression = arith_expression.expression + comma?.expression  + self_arith_expression_sequence?.expression*/     ;
        }


    }
}
