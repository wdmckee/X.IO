using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using X.IO.Arithmetic;
using X.IO.Common;
using X.IO.Common.Word;
using X.IO.Common.Number;
//using static Test.Evaluator;

namespace Test
{
    [TestClass]
    public class Number
    {

        Evaluator evaluator = new Evaluator();


        [TestMethod]
        public void NumberParser()
        {
            evaluator.NumberExpression("77", "77"); // integer-constant
            evaluator.NumberExpression("-77", "-77"); // negative integer
            evaluator.NumberExpression("+77", "+77"); // positive integer
            evaluator.NumberExpression("+--77", "+--77"); // mixed sign integer
            evaluator.NumberExpression("8.88", "8.88"); // decimal
            evaluator.NumberExpression(".99", ".99"); // decimal no leading number
            evaluator.NumberExpression("8.88E-11", "8.88E-11"); //exponent
            evaluator.NumberExpression(".99.88", ".99");







        }








    }
}
