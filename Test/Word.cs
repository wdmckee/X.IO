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
    public class Word
    {

        Evaluator evaluator = new Evaluator();


        [TestMethod]
        public void WordParser()
        {
            evaluator.WordExpression("derek","derek");//no spaces
            evaluator.WordExpression("derek_mckee", "derek_mckee"); //underscores
            evaluator.WordExpression("derek_mckee7899", "derek_mckee"); //gets everything up to the number
        }

        



    


    }
}
