using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
    public class function_parameter
    {


        public string expression { get; }

        
        public parameter_sequence parameter_sequence { get; set; }
        public paren ident_lhs { get; set; }
        public paren ident_rhs { get; set; }        
        public minus minus { get; set; }
        


        public function_parameter(minus _minus, paren _ident_lhs, parameter_sequence _parameter_sequence, paren _ident_rhs)
        {
            minus = _minus;
            ident_lhs = _ident_lhs;
            ident_rhs = _ident_rhs;
            parameter_sequence = _parameter_sequence;

          // if (!error())
            expression = _minus?.expression  + ident_lhs?.expression + parameter_sequence?.expression + ident_rhs?.expression;
            
        }

       

        public bool error()
        {
            if (parameter_sequence == null)
                throw new Exception("function parameters can only accept types contained in parameter_sequence");
            else
                return false;
        }

        }
}
