using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
    public class signed_function
    {

        public string expression { get; }
        public function function { get; set; }
        public minus minus { get; set; }

        public signed_function(minus _minus, function _function)
        {
            minus = _minus;
            function = _function;
            expression = minus?.expression + function.expression;

        }


    }
}
