using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace X.IO.Common.Stack
{
    public class Result
    {

        public string expression { get; }
        public dynamic value { get; set; }


        public Result(dynamic data)
        {
            value = data;
            expression = data?.expression;
        }

      

    }
}
