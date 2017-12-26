using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using X.IO.Arithmetic;
using X.IO.Common;
using X.IO.Common.Word;
using X.IO.Common.Number;


namespace Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Test_ArithmeticParser()
        {
            ArithmeticEvaluator("77", "77"); // integer evaluates to expression
            ArithmeticEvaluator("--(-6+2)", "--(-6+2)"); // Leading negatives
            ArithmeticEvaluator("(5*7/5)+(23)-5*(98-4)/(6*7-42)", "(5*7/5)+(23)-5*(98-4)/(6*7-42)");
            ArithmeticEvaluator("1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)", "1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)");
            ArithmeticEvaluator("sum(77|77,88|88)", "sum(77|77,88|88)");
            ArithmeticEvaluator("foreach(1|2|3,@)", "foreach(1|2|3,@)");
            ArithmeticEvaluator("sum(", "sum(");
            ArithmeticEvaluator("take(1|2|3|4|5,>,3)", "take(1|2|3|4|5,>,3)");
           // ArithmeticEvaluator("foreach(foreach(1|2|3|4|5,@),right((1|2|3|4|5),@))", "foreach(foreach(1|2|3|4|5,@),right((1|2|3|4|5),@))");
        }

        [TestMethod]
        public void Test_ArithmeticInterpreter()
        {


            #region  function_input_type_SingleParam


            #region function_output_type_SingleValue
            // SINGLE-PARAM
            ArithmeticEvaluator("abs(1|4|3|2)", "ERROR:1000", true);
            ArithmeticEvaluator("abs(1|4|3|2,6)", "ERROR:1001", true);
            ArithmeticEvaluator("abs(-4)", "4", true);     
            ArithmeticEvaluator("sin(4)", "0.0697564737441253", true);
            ArithmeticEvaluator("cos(4)", "0.997564050259824", true);
            ArithmeticEvaluator("tan(4)", "0.0699268119435104", true);
            ArithmeticEvaluator("exp(4)", "54.5981500331442", true);
            ArithmeticEvaluator("log(4)", "0.602059991327962", true);
            ArithmeticEvaluator("sqrt(16)", "4", true);
            ArithmeticEvaluator("fibonacci(8)", "21", true);
            // MULTI-PARAM
            ArithmeticEvaluator("mod(10,3)", "1", true);
            ArithmeticEvaluator("mod(10,2)", "0", true);
            ArithmeticEvaluator("round(avg(1|4|3|2),0)", "2", true);
            ArithmeticEvaluator("power(2,3)", "8", true);
            ArithmeticEvaluator("index(4|4|5|6|1,5)", "3", true);
            // PARAMLESS
            ArithmeticEvaluator("pi()", "3.14159265358979", true);
            ArithmeticEvaluator("e()", "2.71828182845905", true);
            #endregion

            #region function_output_type_MultiValue
            // SINGLE-PARAM
            ArithmeticEvaluator("asc(1|4|3|2)", "{1|2|3|4}", true);
            ArithmeticEvaluator("asc(1|4|3|2,3)", "ERROR:1001", true);
            ArithmeticEvaluator("desc(1|4|3|2)", "{4|3|2|1}", true);
            ArithmeticEvaluator("seq(5)", "{1|2|3|4|5}", true);
            ArithmeticEvaluator("distinct(1|4|3|2|1|1|2)", "{1|2|3|4}", true);
            ArithmeticEvaluator("max(1|4|3|2)", "4", true);
            ArithmeticEvaluator("min(1|4|3|2)", "1", true);
            ArithmeticEvaluator("count(1|4|3|2|3)", "5", true);
            ArithmeticEvaluator("sum(1|4|3|2)", "10", true);
            ArithmeticEvaluator("avg(1|4|3|2)", "2.5", true);
            ArithmeticEvaluator("runningtotal(1|4|3|2)", "{1|5|8|10}", true);

            // MULTI-PARAM
            ArithmeticEvaluator("product(1|2|3,1|2|3)", "{1|4|9}", true);
            ArithmeticEvaluator("iif(count(1|2|3|4|4|3|2|1|2|3|1|3|4|5),=,14-1+1,sum(1|2|3),7)", "6", true);
            ArithmeticEvaluator("left(1|2|3|4,2)", "{1|2}", true);
            ArithmeticEvaluator("right(1|2|3|4,2)", "{3|4}", true);
            ArithmeticEvaluator("take(1|2|3|4|5,>,3)", "{4|5}", true);
            #endregion


            //ArithmeticEvaluator("multiply(1|4|3|2)", "", true);
            //ArithmeticEvaluator("divide(1|4|3|2)", "", true);
            //ArithmeticEvaluator("avg(1|4|3|2)", "", true);
            //ArithmeticEvaluator("frac(1|4|3|2)", "", true);
            #endregion


            // RANDOM COMPLEX STUFF
            ArithmeticEvaluator("round(1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)+8.88E-11-100.0,4)", "-1195.6334", true);

            ArithmeticEvaluator("take(1|2|3|10000|11000,>,-1*round(1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)+8.88E-11-100.0,4))", "{10000|11000}", true);




            ////ArithmeticEvaluator("foreach(foreach(1|2|3|4|5,sum(@+1)),right((1|2|3|4|5),@))", "{5|{4,5}|{3,4,5}|{2,3,4,5}|{1,2,3,4,5}}", true);
            ////ArithmeticEvaluator("asc(1,4,3,2,5)", "{1|2|3|4|5}", true); // good example of an error in interpreting
            //ArithmeticEvaluator("asc(1|4|3|2|5)", "{1|2|3|4|5}", true); // Leading negatives
            //ArithmeticEvaluator("multiply(1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)|2)", "-2191.2667", true);
            //ArithmeticEvaluator("--(-6+2)", "-4", true); // Leading negatives
            //ArithmeticEvaluator("(5*7/5)+(23)-5*(98-4)/(6*7-42)", "-∞", true);
            //ArithmeticEvaluator("sum(2|2|2)", "6", true);


            //ArithmeticEvaluator("left(1|2|3|4|5,3)", "{1|2|3}", true);// tuple does not work
            //ArithmeticEvaluator("iif(count(1|2|3|4|4|3|2|1|2|3|1|3|4|5),=,14-1+1,sum(1|2|3),7)", "6", true);// iif
            ////ArithmeticEvaluator("foreach(1|sum(2|3)|3,sum(@|2))", "{3|7|5}", true);
            ////ArithmeticEvaluator("sum(foreach(1|sum(2|3)|3,sum(@|2)))", "15", true);
            //ArithmeticEvaluator("take(1|2|3|4|5,>,3)", "{4|5}", true);
            //ArithmeticEvaluator("count(take(1|2|3|4|5,>,3))", "2", true);
            //ArithmeticEvaluator("round(pi(),2)", "3.14", true);
            ////ArithmeticEvaluator("{runningsum(2|4|6|8|10),@}", "{2|6|12|20|30}", true);

        }



        [TestMethod]
        public void Test_WordParser()
        {
            WordEvaluator("derek","derek");//no spaces
            WordEvaluator("derek_mckee", "derek_mckee"); //underscores
            WordEvaluator("derek_mckee7899", "derek_mckee"); //gets everything up to the number
        }

        [TestMethod]
        public void Test_NumberParser()
        {
            NumberEvaluator("77", "77"); // integer
            NumberEvaluator("-77", "-77"); // negative integer
            NumberEvaluator("+77", "+77"); // positive integer
            NumberEvaluator("+--77", "+--77"); // mixed sign integer
            NumberEvaluator("8.88", "8.88"); // decimal
            NumberEvaluator(".99", ".99"); // decimal no leading number
            NumberEvaluator("8.88E-11", "8.88E-11"); //exponent
            NumberEvaluator(".99.88", ".99");
        }



        private void ArithmeticEvaluator(string _inputexpr, string _inputresult, bool Interpret= false, bool failTest = false)
        {
            var tokens = Helper_ConvertStringToList(_inputexpr);
            int _index = 0;
            ArithmeticParser p = new ArithmeticParser(tokens, ref _index);

            var _actual = p.arith_expression.expression;
            var _expected = _inputresult;


           
            if (Interpret == false && failTest == false)
            {
                Assert.AreEqual(_expected, _actual);
            }

            if (Interpret == true && failTest == false)
            {
                ArithmeticInterpreter i = new ArithmeticInterpreter();
                i.Result(p.result);
                var value = i.ActionList[0].event_data;
                var stack = i.ActionList[1].event_data;
               // if (stack.Count() > 1)
                    Assert.AreEqual(_expected, value.ToString());
                //else
                   // Assert.AreEqual(_expected, Math.Round(value, 4).ToString());
            }



        }

        private void WordEvaluator(string _inputexpr, string _inputresult)
        {
            var tokens = Helper_ConvertStringToList(_inputexpr);
            int _index = 0;
            WordParser p = new WordParser(tokens, ref _index);

            var _actual = p.word.expression;
            var _expected = _inputresult;
            Assert.AreEqual(_expected, _actual);
            
        }

        private void NumberEvaluator(string _inputexpr, string _inputresult)
        {
            var tokens = Helper_ConvertStringToList(_inputexpr);
            int _index = 0;
            NumberParser p = new NumberParser(tokens, ref _index);

            var _actual = p.number.expression;
            var _expected = _inputresult;
            Assert.AreEqual(_expected, _actual);

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
