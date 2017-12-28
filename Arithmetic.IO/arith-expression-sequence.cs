using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
    public class arith_expression_sequence
    {
        /*
        arith-expression-sequence:
	         arith-expression
	        | arith-expression pipe arith-expression-sequence
    */



        public string expression { get; }
        public pipe pipe { get; set; } 
        public arith_expression arith_expression { get; set; }
        public arith_expression_sequence self_arith_expression_sequence { get; set; }

        public external_array external_array { get; set; }

        public arith_expression_sequence(arith_expression _arith_expression)
        {
            arith_expression = _arith_expression;
            expression = arith_expression.expression;
        }

        public arith_expression_sequence(arith_expression _arith_expression, arith_expression_sequence _arith_expression_sequence, pipe _pipe)
        {
            pipe = _pipe;
            arith_expression = _arith_expression;
            self_arith_expression_sequence = _arith_expression_sequence;
            expression = self_arith_expression_sequence?.expression + pipe?.expression + arith_expression.expression; 
          
        }

        public arith_expression_sequence(external_array _external_array)
        {
            external_array = _external_array;
            expression = external_array.expression;
        }







    }
}
