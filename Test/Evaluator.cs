using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using X.IO.Arithmetic;
using X.IO.Common;
using X.IO.Common.Word;
using X.IO.Common.Number;

namespace Test
{
    internal class Evaluator
    {

        
        internal void Arithmetic(string _inputexpr, string _inputresult, Dictionary<string, dynamic> _external_data = null, bool Interpret = false, bool failTest = false)
        {



            var _expected = _inputresult;


            if (Interpret == false && failTest == false)
            {
                var tokens = Helper_ConvertStringToList(_inputexpr);
                int _index = 0;
                ArithmeticParser p = new ArithmeticParser(tokens, ref _index);

                var _actual = p.arith_expression?.expression;


                Assert.AreEqual(_expected, _actual);
            }

            if (Interpret == true && failTest == false)
            {

                X.IO.Arithmetic.Evaluator ii = new X.IO.Arithmetic.Evaluator();
                var _result = ii.Eval(_inputexpr, _external_data);
                Assert.AreEqual(_expected, _result.value.ToString());

            }



        }




        internal void WordExpression(string _inputexpr, string _inputresult)
        {
            var tokens = Helper_ConvertStringToList(_inputexpr);
            int _index = 0;
            WordParser p = new WordParser(tokens, ref _index);

            var _actual = p.word.expression;
            var _expected = _inputresult;
            Assert.AreEqual(_expected, _actual);

        }

        internal void NumberExpression(string _inputexpr, string _inputresult)
        {
            var tokens = Helper_ConvertStringToList(_inputexpr);
            int _index = 0;
            NumberParser p = new NumberParser(tokens, ref _index);

            var _actual = p.number.expression;
            var _expected = _inputresult;
            Assert.AreEqual(_expected, _actual);

           
        }

       








        internal IList<SpecialToken> Helper_ConvertStringToList(string _inputexpr)
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
