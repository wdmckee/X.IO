using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common
{
    public class SpecialToken
    {

        public dynamic Value { get; set; }
        public string StringValue { get; set; }
        public bool IsReserved { get; set; }


        public SpecialToken(dynamic data)
        {
            Value = data;
            StringValue = data.ToString();
            IsReserved = false;
        }

    }
}
