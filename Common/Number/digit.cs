using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace X.IO.Common.Number
{
    public class digit
    {
        public int index { get; }
        public string expression { get; }
        public bool is_digit { get; }        
        public SpecialToken token { get; set; }
        public digit(dynamic data, int _index)
        {
            if    (data.Value.Equals('0')
                || data.Value.Equals('1')
                || data.Value.Equals('2')
                || data.Value.Equals('3')
                || data.Value.Equals('4')
                || data.Value.Equals('5')
                || data.Value.Equals('6')
                || data.Value.Equals('7')
                || data.Value.Equals('8')
                || data.Value.Equals('9')
                )
            {
                is_digit = true;
                token = data;
                expression = data.StringValue;
                index = _index;
            }
            else
            {
                is_digit = false;
            }
        }


    }
}
