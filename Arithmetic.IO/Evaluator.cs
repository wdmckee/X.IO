using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using X.IO.Common;
using X.IO.Common.Stack;
namespace X.IO.Arithmetic
{
    public class Evaluator
    {


        public Result Eval(string expr)
        {
           return  Eval(expr, null);
        }


        public Result Eval(string expr, Dictionary<string, dynamic> external_data = null)
        {

            expr = Regex.Replace(expr, @"\s+", "");
            var tokens = Helper_ConvertStringToList(expr);
            int _index = 0;
            ArithmeticParser p = new ArithmeticParser(tokens, ref _index);

            if (p.arith_expression == null || p.arith_expression.expression != expr) { p.result.IsError = true; p.result.value = ""; return p.result; }
           
           
            ArithmeticInterpreter i = new ArithmeticInterpreter(external_data);
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
