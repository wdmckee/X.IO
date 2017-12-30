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
        public bool IsError { get; set; }

        public Result(dynamic data)
        {
            if (data != null)
                IsError = false;
            else
                IsError = true;



            value = data;

            if (data is string)// other string messages like help strings and such
            { expression = data; }
            else
            { expression = data?.expression; }
                
        }

      

    }
}
