using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class pipe
    {

        public string expression { get; }
        public bool is_pipe { get;  }
        public SpecialToken token { get; set; }
        public pipe(SpecialToken data)
        {
            if (data.StringValue.Equals("|"))
            {
                is_pipe = true;
                token = data;
                expression = data.StringValue;
            }
            else
            {
                is_pipe = false;
            }
        }



    }
}
