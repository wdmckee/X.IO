using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Number
{
    public class number
    {
        public int begin_index { get; }
        public int end_index { get; }
        public string expression { get; }
        public sign sign { get; set; }
        public integer_constant integer_constant { get; set; }
        public floating_point_constant floating_point_constant { get; set; }

        public number(sign _sign, integer_constant _integer_constant)
        {
            sign = _sign;
            integer_constant = _integer_constant;
            expression = sign == null ? integer_constant.expression : sign.expression + integer_constant.expression;
            begin_index = sign == null ? integer_constant.begin_index : sign.index;
            end_index = integer_constant.end_index;
        }

        public number(sign _sign, floating_point_constant _floating_point_constant)
        {
            sign = _sign;
            floating_point_constant = _floating_point_constant;
            expression = sign == null ? floating_point_constant.expression : sign.expression + floating_point_constant.expression;
            begin_index = sign == null ? floating_point_constant.begin_index : sign.index;
            end_index = floating_point_constant.end_index;
        }

    }
}
