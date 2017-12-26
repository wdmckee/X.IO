using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Number
{
    public class number
    {
        public string expression { get; }
        public sign sign { get; set; }
        public integer_constant integer_constant { get; set; }
        public floating_point_constant floating_point_constant { get; set; }

        public number(sign _sign, integer_constant _integer_constant)
        {
            sign = _sign;
            integer_constant = _integer_constant;
            expression = sign == null ? integer_constant.expression : sign.expression + integer_constant.expression;
        }

        public number(sign _sign, floating_point_constant _floating_point_constant)
        {
            sign = _sign;
            floating_point_constant = _floating_point_constant;
            expression = sign == null ? floating_point_constant.expression : sign.expression + floating_point_constant.expression;
        }

    }
}
