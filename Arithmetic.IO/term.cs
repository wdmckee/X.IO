
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
   public class term
    {

        /*
        term: 
	        factor
            | factor wsp* factor-sequence 
    */

        public string expression { get; }
        public factor factor { get; set; }
        public factor_sequence factor_sequence { get; set; }



        public term(factor _factor)
        {
            factor = _factor;
            expression = factor.expression;
        }

        public term(factor _factor, factor_sequence _factor_sequence)
        {
            factor = _factor;
            factor_sequence = _factor_sequence;
            expression =  factor.expression + factor_sequence.expression ;
        }





    }
}
