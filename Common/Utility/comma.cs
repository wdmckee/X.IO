using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace X.IO.Common.Utility
{
    public class comma
    {

        public int index { get; }
        public string expression { get; }
        public bool is_comma { get;  }
        public SpecialToken token { get; set; }
        public comma(SpecialToken data, int _index)
        {
            if (data.StringValue.Equals(","))
            {
                is_comma = true;
                token = data;
                expression = data.StringValue;
                index = _index;
            }
            else
            {
                is_comma = false;
            }
        }



    }
}
