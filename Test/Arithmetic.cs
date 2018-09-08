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
    public class Arithmetic
    {

        Evaluator evaluator = new Evaluator();

        [TestMethod]
        public void ArithmeticParser()
        {

            evaluator.Arithmetic("77", "77"); // integer evaluates to expression
            evaluator.Arithmetic("--(-6+2)", "--(-6+2)"); // Leading negatives
            evaluator.Arithmetic("(5*7/5)+(23)-5*(98-4)/(6*7-42)", "(5*7/5)+(23)-5*(98-4)/(6*7-42)");
            evaluator.Arithmetic("1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)", "1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)");
            evaluator.Arithmetic("sum(77|77,88|88)", "sum(77|77,88|88)");
            evaluator.Arithmetic("foreach(1|2|3,@)", "foreach(1|2|3,@)");
            evaluator.Arithmetic("sum(", null);
            evaluator.Arithmetic("take(1|2|3|4|5,>,3)", "take(1|2|3|4|5,>,3)");
            evaluator.Arithmetic("sum([column])", "sum([column])");
            // ArithmeticEvaluator("foreach(foreach(1|2|3|4|5,@),right((1|2|3|4|5),@))", "foreach(foreach(1|2|3|4|5,@),right((1|2|3|4|5),@))");
        }

        [TestMethod]
        public void ArithmeticInterpreter()
        {
            var _externalData = new Dictionary<string,dynamic>();
            _externalData.Add("column", new List<double> {  1, 2, 3, 4, 5 });






            #region  function_input_type_SingleParam


            #region function_output_type_SingleValue
            // SINGLE-PARAM
            evaluator.Arithmetic("abs(1|4|3|2)", "ERROR:1002", null, true);
            evaluator.Arithmetic("abs(1|4|3|2,6)", "ERROR:1001", null, true);
            evaluator.Arithmetic("abs(-4)", "4", null, true);
            evaluator.Arithmetic("sin(4)", "0.0697564737441253", null, true);
            evaluator.Arithmetic("cos(4)", "0.997564050259824", null, true);
            evaluator.Arithmetic("tan(4)", "0.0699268119435104", null, true);
            evaluator.Arithmetic("exp(4)", "54.5981500331442", null, true);
            evaluator.Arithmetic("log(4)", "0.602059991327962", null, true);
            evaluator.Arithmetic("sqrt(16)", "4", null, true);
            evaluator.Arithmetic("fibonacci(8)", "21", null, true);
            // MULTI-PARAM
            evaluator.Arithmetic("mod(10,3)", "1", null, true);
            evaluator.Arithmetic("mod(10,2)", "0", null, true);
            evaluator.Arithmetic("round(avg(1|4|3|2),0)", "2", null, true);
            evaluator.Arithmetic("power(2,3)", "8", null, true);
            evaluator.Arithmetic("index(4|4|5|6|1,6)", "4", null, true);
            evaluator.Arithmetic("index([column],5)", "5", _externalData, true);
            evaluator.Arithmetic("combine([column],[column])", "{1|2|3|4|5|1|2|3|4|5}", _externalData, true);
            // PARAMLESS
            evaluator.Arithmetic("pi()", "3.14159265358979", null, true);
            evaluator.Arithmetic("e()", "2.71828182845905", null, true);
            #endregion

            #region function_output_type_MultiValue
            // SINGLE-PARAM
            evaluator.Arithmetic("view([column])", "{1|2|3|4|5}", _externalData, true);
            evaluator.Arithmetic("asc(1|4|3|2)", "{1|2|3|4}", null, true);
            evaluator.Arithmetic("asc(1|4|3|2,3)", "ERROR:1001", null, true);
            evaluator.Arithmetic("desc(1|4|3|2)", "{4|3|2|1}", null, true);
            evaluator.Arithmetic("seq(5)", "{1|2|3|4|5}", null, true);
            evaluator.Arithmetic("distinct(1|4|3|2|1|1|2)", "{1|2|3|4}", null, true);
            evaluator.Arithmetic("max(1|4|3|2)", "4", null, true);
            evaluator.Arithmetic("min(1|4|3|2)", "1", null, true);
            evaluator.Arithmetic("count(1|4|3|2|3)", "5", null, true);
            evaluator.Arithmetic("sum(1|4|3|2)", "10", null, true);
            evaluator.Arithmetic("avg(1|4|3|2)", "2.5", null, true);
            evaluator.Arithmetic("runningtotal(1|4|3|2)", "{1|5|8|10}", null, true);

            // MULTI-PARAM
            evaluator.Arithmetic("product(1|2|3,1|2|3)", "{1|4|9}", null, true);
            evaluator.Arithmetic("iif(count(1|2|3|4|4|3|2|1|2|3|1|3|4|5),=,14-1+1,sum(1|2|3),7)", "6", null, true);
            evaluator.Arithmetic("left(1|2|3|4,2)", "{1|2}", null, true);
            evaluator.Arithmetic("right(1|2|3|4,2)", "{3|4}", null, true);
            evaluator.Arithmetic("take(1|2|3|4|5,>,3)", "{4|5}", null, true);
            #endregion


            #endregion


            // RANDOM COMPLEX STUFF
            evaluator.Arithmetic("round(1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)+8.88E-11-100.0,4)", "-1195.6334", null, true);

            evaluator.Arithmetic("take(1|2|3|10000|11000,>,-1*round(1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)+8.88E-11-100.0,4))", "{10000|11000}", null, true);

            evaluator.Arithmetic("take([column],<,sum([column]))", "{1|2|3|4|5}", _externalData, true);
            evaluator.Arithmetic("take([column],<,sum([column])-12)", "{1|2}", _externalData, true);
            evaluator.Arithmetic("iif(1+2,<,5,1,0)+iif(1+2,<,5,1,0)", "2", null, true);


            evaluator.Arithmetic("---(--6+2)", "-8", null, true);
            evaluator.Arithmetic("---(-6+2)", "4", null, true);
            evaluator.Arithmetic("--(-6+2)", "-4",null, true); // Leading negatives
            evaluator.Arithmetic("(2+1)+(-1)", "2", null, true);
            evaluator.Arithmetic("(2 + 1)+ ( - 1 )", "2", null, true);
            evaluator.Arithmetic("(2 + 1)+ (", "3", null, true);
            evaluator.Arithmetic("(2 + A)+ 1", "", null, true);
            evaluator.Arithmetic("(2 + A)+// 1", "", null, true);
            //ArithmeticEvaluator("?", "", null, true);
            evaluator.Arithmetic("/", "", null, true);




            // ALL ITEMS BELOW FIXED IN {1.0.2-beta}
            evaluator.Arithmetic("index(desc([column]),5);", "1", _externalData, true);
            evaluator.Arithmetic("max(seq(1))", "1", null, true);
            evaluator.Arithmetic("max(seq(4))", "4", null, true);
            evaluator.Arithmetic("$t56", "", null, true);
            evaluator.Arithmetic("function(9)", "", null, true);
            evaluator.Arithmetic("(2+3+3)+w", "8", null, true);


            // ALL ITEMS BELOW FIXED IN {1.0.3-beta}
            evaluator.Arithmetic("round(pi())", "ERROR:1002", null, true);
            evaluator.Arithmetic("(9=)", "", null, true);
            evaluator.Arithmetic("take([column_not_exists],<,sum([column])-12)", "ERROR:1003", _externalData, true);

            // ALL ITEMS BELOW FIXED IN {1.1.0-beta}
            evaluator.Arithmetic("sum([a])", "ERROR:1003", null, true);
        }



       


    


    }
}
