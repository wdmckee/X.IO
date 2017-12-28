using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;
using X.IO.Common.Stack;
namespace X.IO.Arithmetic
{
    public class Arithmetic
    {


        public Result Eval(string _inputexpr, Dictionary<string, dynamic> _external_data = null)
        {


            var tokens = Helper_ConvertStringToList(_inputexpr);
            int _index = 0;
            ArithmeticParser p = new ArithmeticParser(tokens, ref _index);

           
           
            ArithmeticInterpreter i = new ArithmeticInterpreter(_external_data);
            i.Result(p.result);
            p.result.value = i.ActionList[0].event_data;


            return p.result;
                
            }


        private IList<SpecialToken> Helper_ConvertStringToList(string _inputexpr)
        {
            IList<SpecialToken> _result = new List<SpecialToken>();
            foreach (char c in _inputexpr)
            {
                var _token = new SpecialToken(c);
                _result.Add(_token);
            }
            return _result;
        }




       


    }
}
