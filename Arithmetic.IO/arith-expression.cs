
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
    public class arith_expression
    {


        public string expression { get; }
        public term term{ get; set; }
        public term_sequence term_sequence { get; set; }



        public arith_expression(term _term)
        {            
            term = _term;            
            expression = term.expression;
        }

        public arith_expression(term _term, term_sequence _term_sequence)
        {           
            term = _term;
            term_sequence = _term_sequence;
            expression = _term.expression  + term_sequence.expression;
        }


    }
}
