
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace X.IO.Common.Utility
{
    public class wsp
    {

        public int index { get; }
        public string expression { get; }
        public bool is_wsp { get;}


        public wsp(SpecialToken data, int _index)
        {
            if (data.StringValue.Equals(" "))
            {
                is_wsp = true;
                expression = data.StringValue;
                index = _index;
            }
            else
            {
                is_wsp = false;
            }
        }



    }
}
