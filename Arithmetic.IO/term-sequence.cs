using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
    public class term_sequence
    {
        /*
          
        term-sequence:
	        infix-operator-type1 term
	        | infix-operator-type1 term term-sequence
         */
       

        public string expression { get; }
        public infix_operator_type1 infix_operator_type1 { get; set; }
        public term term { get; set; }

       
        public term_sequence self_term_sequence { get; set; }

        public term_sequence(infix_operator_type1 _infix_operator_type1, term _term)
        {
            infix_operator_type1 = _infix_operator_type1;
            term = _term;
            expression = _infix_operator_type1.expression + term.expression ;
        }

        public term_sequence(infix_operator_type1 _infix_operator_type1, term _term, term_sequence _term_sequence)
        {
            infix_operator_type1 = _infix_operator_type1;
            term = _term;
            self_term_sequence = _term_sequence;            
            expression = self_term_sequence?.expression + _infix_operator_type1.expression + term.expression;
        }

    }
}
