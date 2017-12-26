
using X.IO.Common.Number;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
    public class factor
    {


        /*
         * 


        */
        public string expression { get; }
        public number number { get; set; }
        public signed_function signed_function {get; set;}
        public iterator iterator { get; set; }
        public function_parameter function_parameter { get; set; }

        public conditional conditional { get; set; }


        public factor( signed_function _signed_function, function_parameter _function_parameter)
        {            
            signed_function = _signed_function;
            function_parameter = _function_parameter;
            expression = signed_function?.expression + function_parameter.expression;           
        }
        
       public factor(number _number)
        {
            number = _number;
            expression = number.expression;

        }


        public factor(iterator _iterator)
        {
            iterator = _iterator;
            expression = iterator.expression;

        }

        public factor(conditional _conditional)
        {
            conditional = _conditional;
            expression = conditional.expression;

        }



    }
}
