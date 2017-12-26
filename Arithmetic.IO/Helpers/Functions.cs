using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic.Helpers
{
    public class Functions
    {

        Dictionary<string, string> _tupleFunctions = new Dictionary<string, string>();
        Dictionary<string, string> _valueFunctions = new Dictionary<string, string>();


        public List<string> operator_input_type_infix = new List<string>();

        public List<string> function_input_type_SingleParam = new List<string>();
        public List<string> function_input_type_MultiParam = new List<string>();
        public List<string> function_input_type_Paramless = new List<string>();

        public List<string> function_output_type_SingleValue = new List<string>();
        public List<string> function_output_type_MultiValue = new List<string>();



        public  Functions()
        {
            //LoadExternalFunctions();
            //LoadInternalFunctions();

            LoadInfixInput();
            LoadSingleParamInput();
            LoadMultiParamInput();
            LoadParamlessInput();


            LoadSingleValueOutputs();
            LoadMultiValueOutputs();


        }



        public string ReturnValueFunction(string op)
        {
            foreach (var kvp in _valueFunctions)
            {
                if (op == kvp.Key)
                {
                    var fn = kvp.Value;
                    return fn;                    
                }
            }
            return null;
        }


        public string ReturnTupleFunction(string op)
        {
            foreach (var kvp in _tupleFunctions)
            {
                if (op == kvp.Key)
                {
                    var fn = kvp.Value;
                    return fn;
                }
            }
            return null;
        }


        public bool ReturnFunctionName(string key)
        {
            bool value;
            value = _valueFunctions.ContainsKey(key);

            if (value == false)
            {
                value = _tupleFunctions.ContainsKey(key);
            }

            return value;
        }

        


        private void LoadExternalFunctions()
        {
           

            _valueFunctions.Add("stdevs", "sqrt(sum({{$},(avg($)-@)},power(@|2))/(count($)-1))");
            _valueFunctions.Add("stdevp", "sqrt(sum({{$},(avg($)-@)},power(@|2))/(count($)))");
            _tupleFunctions.Add("zscore", "({({$},(avg($)-@))},(@/stdevp($)))");
            _tupleFunctions.Add("tuple", "({$},@)");
            _tupleFunctions.Add("pearson", "round(sum(product(zscore(parameter($|1))|zscore(parameter($|2))))/count(parameter($|1))|2)");//         
            _tupleFunctions.Add("mean", "avg($)");
            _tupleFunctions.Add("_median_odd", "index(asc($)|count($)/2)");
            _tupleFunctions.Add("_median_even", "(index(asc($)|round(count($)/2|0))+index(asc($)|round(count($)/2|0)+1))/2");
            _tupleFunctions.Add("median", "iif(mod(count($)|2)|=|0|_median_even($)|_median_odd($))");
            _tupleFunctions.Add("isnormal", "iif(iif(median($)|=|mean($)|median($)|0)|=|mode($)|1|0)");

       

            // WORKING FUNCTIONS .........................

            //.............................................

            function_input_type_MultiParam.Add("stdevs");
            function_input_type_MultiParam.Add("stdevp");
            function_input_type_MultiParam.Add("zscore");
            function_input_type_MultiParam.Add("tuple");
            function_input_type_MultiParam.Add("pearson");
            function_input_type_MultiParam.Add("mean");
            function_input_type_MultiParam.Add("median");
            function_input_type_MultiParam.Add("_median_even");
            function_input_type_MultiParam.Add("_median_odd");
            function_input_type_MultiParam.Add("isnormal");


        }


        private void LoadInternalFunctions()
        {
           
            

            // we will worry about these in a bit
            //function_input_type_MultiParam.Add("sturges");
            
            //function_input_type_MultiParam.Add("colset");
            
            //function_input_type_MultiParam.Add("nest");
            //function_input_type_MultiParam.Add("runningsum");
            //function_input_type_MultiParam.Add("frequency");
            //function_input_type_MultiParam.Add("mode");
            //function_input_type_MultiParam.Add("parameter");
            //function_input_type_MultiParam.Add("foreach");





        }


   



        private void LoadSingleParamInput()
        {
            function_input_type_SingleParam.Add("abs");
            function_input_type_SingleParam.Add("frac");
            function_input_type_SingleParam.Add("sin");
            function_input_type_SingleParam.Add("cos");
            function_input_type_SingleParam.Add("tan");
            function_input_type_SingleParam.Add("exp");
            function_input_type_SingleParam.Add("log");
            function_input_type_SingleParam.Add("fibonacci");
            function_input_type_SingleParam.Add("sqrt");
            function_input_type_SingleParam.Add("seq");
            function_input_type_SingleParam.Add("asc");
            function_input_type_SingleParam.Add("multiply");
            function_input_type_SingleParam.Add("divide");
            function_input_type_SingleParam.Add("sum");
            function_input_type_SingleParam.Add("max");
            function_input_type_SingleParam.Add("min");
            function_input_type_SingleParam.Add("count");
            function_input_type_SingleParam.Add("avg");
            function_input_type_SingleParam.Add("distinct");
            function_input_type_SingleParam.Add("desc");
            function_input_type_SingleParam.Add("runningtotal");
        }

        private void LoadMultiParamInput()
        {
            function_input_type_MultiParam.Add("round");
            function_input_type_MultiParam.Add("power");
            function_input_type_MultiParam.Add("product");
            function_input_type_MultiParam.Add("left");
            function_input_type_MultiParam.Add("right");
            function_input_type_MultiParam.Add("take");
            function_input_type_MultiParam.Add("index");
            function_input_type_MultiParam.Add("mod");
            function_input_type_MultiParam.Add("iif");
            function_input_type_MultiParam.Add("findindex");
        }
        
        private void LoadParamlessInput()
        {
            function_input_type_Paramless.Add("pi");
            function_input_type_Paramless.Add("e");
        }

        private void LoadInfixInput()
        {
            operator_input_type_infix.Add("+");//"+", "-", "*", "/"
            operator_input_type_infix.Add("-");
            operator_input_type_infix.Add("*");
            operator_input_type_infix.Add("/");
        }






        private void LoadSingleValueOutputs()
        {
            function_output_type_SingleValue.Add("+");
            function_output_type_SingleValue.Add("-");
            function_output_type_SingleValue.Add("*");
            function_output_type_SingleValue.Add("/");
            function_output_type_SingleValue.Add("abs");
            function_output_type_SingleValue.Add("frac");
            function_output_type_SingleValue.Add("sin");
            function_output_type_SingleValue.Add("cos");
            function_output_type_SingleValue.Add("tan");
            function_output_type_SingleValue.Add("exp");
            function_output_type_SingleValue.Add("log");
            function_output_type_SingleValue.Add("fibonacci");
            function_output_type_SingleValue.Add("sqrt");
            function_output_type_SingleValue.Add("multiply");
            function_output_type_SingleValue.Add("divide");
            function_output_type_SingleValue.Add("sum");
            function_output_type_SingleValue.Add("max");
            function_output_type_SingleValue.Add("min");
            function_output_type_SingleValue.Add("count");
            function_output_type_SingleValue.Add("avg");
            function_output_type_SingleValue.Add("round");
            function_output_type_SingleValue.Add("power");
            function_output_type_SingleValue.Add("index");
            function_output_type_SingleValue.Add("pi");
            function_output_type_SingleValue.Add("e");
            function_output_type_SingleValue.Add("mod");
            function_output_type_SingleValue.Add("iif");
            function_output_type_SingleValue.Add("findindex");
            function_output_type_SingleValue.Add("mode");


        }

        private void LoadMultiValueOutputs()
        {
            function_output_type_MultiValue.Add("seq");
            function_output_type_MultiValue.Add("sturges");
            function_output_type_MultiValue.Add("product");
            function_output_type_MultiValue.Add("left");
            function_output_type_MultiValue.Add("right");
            function_output_type_MultiValue.Add("take");
            function_output_type_MultiValue.Add("distinct");
            function_output_type_MultiValue.Add("asc");
            function_output_type_MultiValue.Add("desc");
            function_output_type_MultiValue.Add("colset");
            function_output_type_MultiValue.Add("nest");
            function_output_type_MultiValue.Add("runningsum");
            function_output_type_MultiValue.Add("frequency");
            function_output_type_MultiValue.Add("foreach");
            function_output_type_MultiValue.Add("parameter");
        }


    }
}
