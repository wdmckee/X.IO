
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class iterator
    {


        public string expression { get; }
        public bool is_iterator { get; }
        public SpecialToken token { get; set; }
        public iterator(dynamic data)
        {
            if (data.StringValue.Equals("@"))
            {
                is_iterator = true;
                token = data;
                expression = data.StringValue;
            }
            else
            {
                is_iterator = false;
            }
        }




    }
}
