
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace X.IO.Common.Utility
{
    public class wsp
    {
        public string expression { get; }
        public bool is_wsp { get;}


        public wsp(SpecialToken data)
        {
            if (data.StringValue.Equals(" "))
            {
                is_wsp = true;
                expression = data.StringValue;
            }
            else
            {
                is_wsp = false;
            }
        }



    }
}
