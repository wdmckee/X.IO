using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class ArithmeticScanner
    {





        public IList<SpecialToken> Scan(string _inputexpr)
        {
            var expr = Regex.Replace(_inputexpr, @"\s+", "");

            IList<SpecialToken> _result = new List<SpecialToken>();
            foreach (char c in expr)
            {
                // ALLOWED LIST
                if (
                    char.IsLetterOrDigit(c)
                    || c == '*'
                    || c == '/'
                    || c == '-'
                    || c == '+'
                    || c == '@'
                    || c == '('
                    || c == ')'
                    || c == '['
                    || c == ']'
                    || c == '|'
                    || c == '='
                    || c == '<'
                    || c == '>'
                    || c == ','
                    || c == '.'
                    )
                {
                    var _token = new SpecialToken(c);
                    _result.Add(_token);
                }
            }
            return _result;
        }



    }
}
