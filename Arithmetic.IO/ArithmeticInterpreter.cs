using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using X.IO.Arithmetic.Helpers;
using X.IO.Common.Stack;
using X.IO.Common.Number;
using Arithmetic.IO.Helpers;

namespace X.IO.Arithmetic
{
    public class ArithmeticInterpreter
    {

        public Dictionary<string, List<double>> _externalData = new Dictionary<string, List<double>>();
        public List<Arithmetic_ActionData> ActionList = new List<Arithmetic_ActionData>();
        OpStack _opStack = new OpStack();
        //OpStack _delayedfnStack = new OpStack();
        OpStack _resultStack = new OpStack();
        //Functions extern_fn = new Functions();
        InterpreterPropertyHelper iph;
        bool isError = false;

        string expression;

        public  ArithmeticInterpreter(OpStack opStack)
        {
            _opStack = opStack.Copy();
           // _delayedfnStack = opStack;
        }

        public ArithmeticInterpreter()
        {

        }
        public ArithmeticInterpreter(ref Dictionary<string, List<double>> ExternalData)
        {
            _externalData = ExternalData;
        }
        public void Result(dynamic result)
        {
            
            expression = result.expression;
            Console.WriteLine("{0}{1}","expr:", expression);
            _resultStack.Push(result);
            top:
            // NOTICE BOTTOM UP METHODOLOGY OF INTERPRETER            
            dynamic _result = _resultStack.PeekBottom();


            #region Stack Operators
            #region number
            // TREATING THIS AS ATOMIC LEVEL
            if (_result is number)
            {
                var _temp = _result;
                var _sign = 1;
                dynamic _value;

                if (_result.sign.token != null)
                {
                    _sign = _result?.sign.token.StringValue == "-" ? -1 : 1;
                }


                if (_result.integer_constant != null)
                {
                    _value = int.Parse(_result.integer_constant.expression);

                    result = _result.integer_constant;
                    _result.integer_constant = null;
                    _resultStack.ReplaceBottom(_result);
                    _opStack.Push(_value * _sign);
                    _resultStack.Push(_value * _sign);
                    goto top;
                }

                _result = _temp;


                if (_result.floating_point_constant != null)
                {
                    _value = Single.Parse(_result.floating_point_constant.expression);

                    result = _result.floating_point_constant;
                    _result.floating_point_constant = null;
                    _resultStack.ReplaceBottom(_result);
                    _opStack.Push(_value * _sign);

                    goto top;
                }

                
                result = _resultStack.PopBottomPeek();
                goto top;
            }
            #endregion
            #region iterator
            if (_result is iterator)
            {
                var _temp = _result;
                dynamic _value;




                if (_result.is_iterator != null)
                {
                    _value = _result.expression;

                    result = _result;
                    _result = null;
                    _resultStack.ReplaceBottom(_result);
                    _opStack.Push(_value);
                    _resultStack.Push(_value);
                    goto top;
                }

                _result = _temp;


                result = _resultStack.PopBottomPeek();
                goto top;
            }
            #endregion          
            #region function
            
            if (_result is function)
            {
                var _temp = _result;
                dynamic _value;




                if (_result.word != null)
                {
                    _value = _result.expression;

                    result = _result;
                    _result = null;
                    _resultStack.ReplaceBottom(_result);
                    _opStack.Push(_value);
                    _resultStack.Push(_value);
                    goto top;
                }

                _result = _temp;


                result = _resultStack.PopBottomPeek();
                goto top;

            }
            #endregion
            #region external-array
            if (_result is external_array)
            {
                var _temp = _result;
                dynamic _value;




                if (_result.word != null)
                {
                    _value = _result.word.expression;

                    result = _result;
                    _result = null;
                    _resultStack.ReplaceBottom(_result);
                   // _opStack.Push("end");
                    _opStack.Push(_value);
                    //_opStack.Push("external");
                    _resultStack.Push(_value);
                    goto top;
                }

                _result = _temp;


                result = _resultStack.PopBottomPeek();
                goto top;
            }
            #endregion
            #region conditional
            if (_result is conditional)
            {
                var _temp = _result;
                dynamic _value;




                if (_result.is_conditional != null)
                {
                    _value = _result.expression;

                    result = _result;
                    _result = null;
                    _resultStack.ReplaceBottom(_result);
                    _opStack.Push(_value);
                    _resultStack.Push(_value);
                    goto top;
                }

                _result = _temp;


                result = _resultStack.PopBottomPeek();
                goto top;
            }
            #endregion
            #endregion

           

            #region signed-function
                // TREATING THIS AS ATOMIC LEVEL
                if (_result is signed_function)
            {
                var _temp = _result;





                if (_result.function != null)
                {
                    
                    result = _result.function;
                    _result.function = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;

                }

                _result = _temp;

                if (_result.minus != null)
                {

                    _opStack.Push(_result.minus.expression);
                    _result.minus = null;

                }
                result = _resultStack.PopBottomPeek();
                goto top;
            }
            #endregion

            #region arith-expression-sequence
            if (_result is arith_expression_sequence)
            {
                var _temp = _result;

                

                if (_result.self_arith_expression_sequence != null)
                {
                    
                    result = _result.self_arith_expression_sequence;
                    _result.self_arith_expression_sequence = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                    

                    goto top;
                }

                _result = _temp;


                if (_result.arith_expression != null)
                {

                    result = _result.arith_expression;
                    _result.arith_expression = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;
                }

                _result = _temp;


                if (_result.external_array != null)
                {

                    result = _result.external_array;
                    _result.external_array = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;
                }

                _result = _temp;



                result = _resultStack.PopBottomPeek();
                
                goto top;
            }
            #endregion

            #region parameter-sequence
            if (_result is parameter_sequence)
            {
                var _temp = _result;



                if (_result.self_parameter_sequence != null)
                {

                    result = _result.self_parameter_sequence;
                    _result.self_parameter_sequence = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);


                    goto top;
                }

                _result = _temp;


                // this is partial;y done, needs to be places on the _resultStack too I think
                if (_result.comma != null)
                {
                    _result.comma = null;
                    _opStack.Push(",");
                }
                _result = _temp;



                if (_result.arith_expression_sequence != null)
                {

                    result = _result.arith_expression_sequence;
                    _result.arith_expression_sequence = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;
                }

                _result = _temp;


                


               result = _resultStack.PopBottomPeek();

                goto top;
            }
            #endregion

            
            #region function-parameter
            if (_result is function_parameter)
            {
                var _temp = _result;
                


                if (_result.parameter_sequence != null)
                {
                    
                    result = _result.parameter_sequence;
                    _result.parameter_sequence = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;
                }

                _result = _temp;

                if (_result.minus != null)
                {

                    _opStack.Push(_result.minus.expression);
                    _result.minus = null;
                    goto top;
                }

                _result = _temp;
                


                    result = _resultStack.PopBottomPeek();
                goto top;

            }
            #endregion

            #region factor
            if (_result is factor)
            {
                var _temp = _result;

                

                if (_result.number != null)
                {

                    result = _result.number;
                    _result.number = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;
                }               

                _result = _temp;


                if (_result.iterator != null)
                {

                    result = _result.iterator;
                    _result.iterator = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;
                }

                _result = _temp;



                if (_result.conditional != null)
                {

                    result = _result.conditional;
                    _result.conditional = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;
                }

                _result = _temp;



                if (_result.function_parameter != null)
                {
                    // do a look ahead here due to the note following this code chuck
                    if (_result.signed_function != null) { _opStack.Push("end"); }
                    
                    result = _result.function_parameter;
                    _result.function_parameter = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);                   

                   goto top;
                }

                _result = _temp;


                

                // the only place where something like this happens
                // our function gets interpreted AFTER our expression despite the fact that
                // it comes before it in the bnf. this is because the function does not contain
                // other members (no children) and is simply an identifier
                if (_result.signed_function != null)
                {
                    
                    result = _result.signed_function;
                    _result.signed_function = null;

                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                 
                    goto top;
                }

                _result = _temp;






                result = _resultStack.PopBottomPeek();
                goto top;
            }
            #endregion
            #region factor-sequence - ACTION
            if (_result is factor_sequence)
            {
                var _temp = _result;

                // note: self always comes first
                if (_result.self_factor_sequence != null)
                {

                    result = _result.self_factor_sequence;
                    _result.self_factor_sequence = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                    goto top;
                }

                _result = _temp;


                if (_result.factor != null)
                {

                    result = _result.factor;
                    _result.factor = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                    goto top;
                }

                _result = _temp;
                // action


                _opStack.Push(_result.infix_operator_type2.expression);


                result = _resultStack.PopBottomPeek();
                goto top;

            }
            #endregion


            #region term
            if (_result is term)
            {
                var _temp = _result;

                

                if (_result.factor != null)
                {

                    result = _result.factor;
                    _result.factor = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);                    
                    goto top;
                }

                _result = _temp;



                if (_result.factor_sequence != null)
                {

                    result = _result.factor_sequence;
                    _result.factor_sequence = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                    goto top;

                }


                _result = _temp;

                // _opStack.Push("term");
                //calculate();

                result = _resultStack.PopBottomPeek();
                goto top;
            }
            #endregion
            #region term-sequence - ACTION
            if (_result is term_sequence)
            {
                var _temp = _result;

                // note: self always comes first
                if (_result.self_term_sequence != null)
                {

                    result = _result.self_term_sequence;
                    _result.self_term_sequence = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                    goto top;
                }

                _result = _temp;


                if (_result.term != null)
                {

                    result = _result.term;
                    _result.term = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                    goto top;
                }

                _result = _temp;

                _opStack.Push(_result.infix_operator_type1.expression);




                result = _resultStack.PopBottomPeek();
                goto top;

            }
            #endregion
            
            
            #region arith_expression
            if (_result is arith_expression)
            {
                var _temp = _result;

                // note: self always comes first
               

                if (_result.term != null)
                {

                    result = _result.term;
                    _result.term = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                    goto top;
                }

                _result = _temp;




                if (_result.term_sequence != null)
                {

                    result = _result.term_sequence;
                    _result.term_sequence = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);
                    goto top;
                }

                _result = _temp;




                              
                result = _resultStack.PopBottomPeek();
                goto top;

            }
            #endregion            

            #region Result
            if (_result is Result)
            {
                var _temp = _result;

                if (_result.value != null)
                {
                    result = _result.value;
                    _result.value = null;
                    _resultStack.ReplaceBottom(_result);
                    _resultStack.Push(result);

                    goto top;
                }

                _result = _temp;

                calculate();
                return;
                //result = _resultStack.PopBottom();
                //goto top;
            }
            #endregion



            if (_resultStack.Count() > 0)
            {
                result = _resultStack.PopBottomPeek();
                goto top;
            }

            

        }






        #region HELPERS

        /*

	ArithmeticInterpreter
	Calculate()
		SearchSegment()
			GetSegment()
		StatusEvaluations()
		DoUnaryOperation()
		DoMultiStepOperation()
		CalculateValue()	
			GetCumulative()
				ReplaceApplyFn()
					Calculate()

         */



        private void calculate()
        {


            // HERE WE NOW HAVE POST-FIX OPERATORS. SEARCH FROM TOP TO BOTTOM.



            // un-comment this again if we have issues with where the calulates are
            again:

            

            iph = new InterpreterPropertyHelper(_opStack, _externalData);
            iph.StatusEvaluations();
      
            if(!iph.isError)
                CalculateValue(ref iph);



            #region display and actions
            // note the peekbottom function now references the top of the stack since its actually looking for the last added which was pushed to the top
            //Console.WriteLine("{0} {1} {2} {3} = {4}", "step: ", iph._number_1, iph._current_operation, iph._number_2, _opStack.PeekBottom()); // our answer

            if (_opStack.Count() > 1 && iph.operation_done_last_round && !isError)
                goto again;




            #region new code for tuple
            dynamic _return_value = null;
            if (_opStack.Count() > 1)
            { _return_value = _opStack.TupleBuilder(); }
            else
            { _return_value = _opStack.PopTop(); }
            #endregion

            if (_return_value != null)
            {
                ActionList.Add(new Arithmetic_ActionData(_return_value));
                ActionList.Add(new Arithmetic_ActionData(_opStack));
            }
            else
                ActionList.Add(new Arithmetic_ActionData(expression));
            #endregion

        }



     

        private void CalculateValue(ref InterpreterPropertyHelper iph)
        {
            #region operations
            double _value;
            int paramCount = iph.li_paramsLoc.Count();


            switch (iph._current_operation)
            {
                case "+":
                    _opStack.PushAt(iph.NextTuplePush, iph._number_1 + iph._number_2);
                    break;
                case "-":
                    _opStack.PushAt(iph.NextTuplePush, iph._number_1 - iph._number_2);
                    break;
                case "*":
                    _opStack.PushAt(iph.NextTuplePush, iph._number_1 * iph._number_2);
                    break;
                case "/":
                    _opStack.PushAt(iph.NextTuplePush, iph._number_1 / iph._number_2);
                    break;

                #region function_output_type_SingleValue
                case "abs":
                    CalculateAbs();
                    break;
                case "frac":
                    _opStack.PushAt(iph.NextTuplePush, DoubleToFraction(iph._number_2));
                    break;
                case "sin":
                    CalculateSin();
                    break;
                case "cos":
                    CalculateCos();
                    break;
                case "tan":
                    CalculateTan();
                    break;
                case "exp":
                    CalculateExp();
                    break;
                case "log":
                    CalculateLog();
                    break;
                case "sqrt":
                    CalculateSqrt();                   
                    break;
                case "fibonacci":
                    CalculateFib();
                    break;
                case "pi":
                    CalculatePi();
                    break;
                case "e":
                    CalculateEuler();
                    break;
                case "max":
                    CalculateMax();
                    break;
                case "min":
                    CalculateMin();
                    break;
                case "count":
                    CalculateCount();
                    break;
                case "sum":
                    CalculateSum();
                    break;
                case "avg":
                    CalculateAvg();
                    break;
                case "mod":
                    CalculateMod();
                    break;
                case "round":
                    CalculateRound();
                    break;
                case "power":
                    CalculatePower();
                    break;
                case "left":
                    CalculateLeft();                   
                    break;
                case "right":
                    CalculateRight();
                    break;
                case "index":
                    CalculateIndex();                   
                    break;

                #endregion

                #region function_output_type_MultiValue
                case "asc":
                    CalculateAsc();
                    break;
                case "desc":
                    CalculateDesc();
                    break;
                case "seq":
                    CalculateSeq();
                    break;
                case "distinct":
                    CalculateDistinct();
                    break;
                case "product":
                    CalculateProduct();                   
                    break;
                case "iif": //iif(<expr>,<op>,<comarison>,T,F) 
                    CalculateIIF();
                    break;
                case "take": //take(<expr>,<op>,<comarison>) ex. take({1,2,3}|<|3)
                    CalculateTake();                   
                    break;
                case "runningtotal":
                    CalculateRunningTotal();                    
                    break;

                #endregion

                #region not yet classified




                case "multiply":
                    if (paramCount != 1) { ExpectedParameterError(paramCount, "multiply"); }
                    _value = GetCumulative(iph.li_params, "*");
                    _opStack.PushAt(iph.NextTuplePush, _value);
                    break;
                case "divide":
                    if (paramCount != 1) { ExpectedParameterError(paramCount, "divide"); }
                    _value = GetCumulative(iph.li_params, "/");
                    _opStack.PushAt(iph.NextTuplePush, _value);
                    break;
               
               

             




                




              
               

                
               

               

               
                case "parameter":
                    var _param = ReturnParameter(1);
                    // we don't supply a param for this one only
                    PushListToStack(_param);
                    break;
              
                // ADD FUNCTION
                case "findindex":
                    var _findindex_param = ReturnParameter(1);
                    var _findindex_value_to_find = ReturnParameter(2)[0];

                    _findindex_param.Reverse();
                    List<double> _findindex_tuple = new List<double>();
                    int _findindex_index = 0;
                    foreach (var item in _findindex_param)
                    {
                        if (item == _findindex_value_to_find)
                        {
                            _findindex_tuple.Add(_findindex_index+1);
                        }
                        _findindex_index++;
                    }
                    if (_findindex_index == 0) { throw new Exception("index not found in collection"); }
                    PushListToStack(_findindex_tuple);
                    break;

                case "nest":
                    var _nest_outer_param = ReturnParameter(1);
                    var _nest_inner_param = ReturnParameter(2);
                   


                    var _nest_outer_param_count = 0;
                    foreach (var item in _nest_outer_param)
                    {
                        var _new_value = "{"+item.ToString()+"|"+ _nest_inner_param[_nest_outer_param_count].ToString() + "}" ;
                        _opStack.PushAt(iph.NextTuplePush- _nest_outer_param_count, _new_value);
                        _nest_outer_param_count++;
                    }

                    break;

               

                case "frequency":
                    var _frequency_list = ReturnParameter(1);
                    var _frequency_data = ReturnParameter(2);

                    int counter = 0;
                    foreach (var item in _frequency_list)
                    {
                       var _count_freq =  _frequency_data.Where(x => x == item).Count();
                       var _new_freq_value = "{" + item.ToString() + "|" + _count_freq.ToString() + "}";
                        _opStack.PushAt(iph.NextTuplePush - counter, _new_freq_value);
                        counter++;
                    }
                    
                    break;

                case "mode":
                    var _mode_data = ReturnParameter(1);

                    double mode = _mode_data.GroupBy(i => i)  //Grouping same items
                        .OrderByDescending(g => g.Count()) //now getting frequency of a value
                        .Select(g => g.Key) //selecting key of the group                        
                        .FirstOrDefault();   //Finally, taking the most frequent value

                    _opStack.PushAt(iph.NextTuplePush, mode);
                    break;

                    #endregion


            }


           

            #endregion

        }

        

        private void ExpectedParameterError(int paramCount, string fn_name)
        {
            throw new Exception(string.Format("{0} of params was supplied to the function {1}", paramCount, fn_name));
        }

        public double GetCumulative(List<double> input, string type)// make this an enum later
        {
            input.Reverse();
            var returnValue = 0.0;
            int _iterator = 0;
            var segment = -1;
            //foreach (double current in input)
            do
            {
                double current = input[_iterator];
                switch (type)
                {
                    case "+":
                        returnValue += current;
                        break;
                    case "*":
                        if (_iterator > 0)
                            returnValue *= current;
                        else
                            returnValue = current;
                        break;
                    case "/":
                        if (_iterator > 0 && current > 0)
                            returnValue /= current;
                        else
                            returnValue = current;
                        break;
                    case "max":
                        returnValue += Math.Max(current, returnValue) - returnValue;
                        break;
                    case "min":
                        if (_iterator > 0)
                            returnValue += Math.Min(current, returnValue) - returnValue;
                        else
                            returnValue = current;
                        break;
                    case "count":
                        returnValue += 1;
                        break;

                    //case "product":
                    //    if(_iterator < (input.Count / 2)) // we only move halfway through our list
                    //    runningTotal += current * input[(input.Count/2)+ _iterator]; 
                    //    break;








                        // ADD FUNCTION
                }

                _iterator++;
            } while (_iterator < input.Count);
            return returnValue;
        }



        #endregion




        #region FUNCTIONS

        #region  function_input_type_SingleParam

        // OUTPUT SINGLEVALUE
        private void CalculateAbs()
        {
            // ONLY TAKE A SINGLE VALUE, DO NOT ACCEPT AN ARRAY AS A PARAMETER
            if (iph.li_params.Count == 1)
            {
                var _value = iph.li_params[0];
                _opStack.PushAt(iph.NextTuplePush, Math.Abs(_value));
            }
            else
            {                
                _opStack.Clear();
                _opStack.Push("ERROR:1000");
                isError = true;
                //throw new Exception(string.Format("looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter"));
            }

        }
        private void CalculateSin()
        {
            // ONLY TAKE A SINGLE VALUE, DO NOT ACCEPT AN ARRAY AS A PARAMETER
            if (iph.li_params.Count == 1)
            {
                var _value = iph.li_params[0];
                _opStack.PushAt(iph.NextTuplePush, Math.Sin(Math.PI / 180 * _value));
            }
            else
            {
                _opStack.Clear();
                _opStack.Push("ERROR:1000");
                isError = true;
                //throw new Exception(string.Format("looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter"));
            }

        }
        private void CalculateCos()
        {
            // ONLY TAKE A SINGLE VALUE, DO NOT ACCEPT AN ARRAY AS A PARAMETER
            if (iph.li_params.Count == 1)
            {
                var _value = iph.li_params[0];
                _opStack.PushAt(iph.NextTuplePush, Math.Cos(Math.PI / 180 * _value));
            }
            else
            {
                _opStack.Clear();
                _opStack.Push("ERROR:1000");
                isError = true;
                //throw new Exception(string.Format("looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter"));
            }

        }
        private void CalculateTan()
        {
            // ONLY TAKE A SINGLE VALUE, DO NOT ACCEPT AN ARRAY AS A PARAMETER
            if (iph.li_params.Count == 1)
            {
                var _value = iph.li_params[0];
                _opStack.PushAt(iph.NextTuplePush, Math.Tan(Math.PI / 180 * _value));
            }
            else
            {
                _opStack.Clear();
                _opStack.Push("ERROR:1000");
                isError = true;
                //throw new Exception(string.Format("looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter"));
            }

        }
        private void CalculateExp()
        {
            // ONLY TAKE A SINGLE VALUE, DO NOT ACCEPT AN ARRAY AS A PARAMETER
            if (iph.li_params.Count == 1)
            {
                var _value = iph.li_params[0];
                _opStack.PushAt(iph.NextTuplePush, Math.Exp(_value));
            }
            else
            {
                _opStack.Clear();
                _opStack.Push("ERROR:1000");
                isError = true;
                //throw new Exception(string.Format("looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter"));
            }

        }
        private void CalculateLog()
        {
            // ONLY TAKE A SINGLE VALUE, DO NOT ACCEPT AN ARRAY AS A PARAMETER
            if (iph.li_params.Count == 1)
            {
                var _value = iph.li_params[0];
                _opStack.PushAt(iph.NextTuplePush, Math.Log10(_value));
            }
            else
            {
                _opStack.Clear();
                _opStack.Push("ERROR:1000");
                isError = true;
                //throw new Exception(string.Format("looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter"));
            }
            
        }
        private void CalculateSqrt()
        {
            // ONLY TAKE A SINGLE VALUE, DO NOT ACCEPT AN ARRAY AS A PARAMETER
            if (iph.li_params.Count == 1)
            {
                var _value = iph.li_params[0];
                _opStack.PushAt(iph.NextTuplePush, Math.Sqrt(_value));
            }
            else
            {
                _opStack.Clear();
                _opStack.Push("ERROR:1000");
                isError = true;
                //throw new Exception(string.Format("looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter"));
            }

        }
        private void CalculateFib()
        {
            // ONLY TAKE A SINGLE VALUE, DO NOT ACCEPT AN ARRAY AS A PARAMETER
            if (iph.li_params.Count == 1)
            {
                var _value = iph.li_params[0];

                int n = Convert.ToInt32(_value);
                int[] fib = new int[n + 1];
                fib[0] = 0;
                fib[1] = 1;

                for (int i = 2; i <= n; i++)
                {
                    fib[i] = fib[i - 1] + fib[i - 2];
                }

                _opStack.PushAt(iph.NextTuplePush, Convert.ToDouble(fib[n]));
            }
            else
            {
                _opStack.Clear();
                _opStack.Push("ERROR:1000");
                isError = true;
                //throw new Exception(string.Format("looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter"));
            }

        }
        private void CalculateMod()
        {
            if (iph.li_paramsLoc.Count() == 2)
            {
                var param1 = ReturnParameter(1)[0]; // we only accept single value
                var param2 = ReturnParameter(2)[0];
                _opStack.PushAt(iph.NextTuplePush, param1 % param2);
            }
        }
        private void CalculateIndex()
        {
            var _param1 = ReturnParameter(1);
            var _param2 = ReturnParameter(2);
            _param1.Reverse();
            var _value = _param1.FindIndex(x => x == _param2[0]) +1;           
            _opStack.PushAt(iph.NextTuplePush, _value);

        }
        private void CalculateMax()
        {
            if (iph.li_paramsLoc.Count() == 1)
            {
                var _value = GetCumulative(iph.li_params, "max");
                _opStack.PushAt(iph.NextTuplePush, _value);
            }
        }
        private void CalculateMin()
        {
            if (iph.li_paramsLoc.Count() == 1)
            {
                var _value = GetCumulative(iph.li_params, "min");
                _opStack.PushAt(iph.NextTuplePush, _value);
            }
        }
        private void CalculateCount()
        {
            if (iph.li_paramsLoc.Count() == 1)
            {
                var _value = GetCumulative(iph.li_params, "count");
                _opStack.PushAt(iph.NextTuplePush, _value);
            }
        }
        private void CalculateSum()
        {
            if (iph.li_paramsLoc.Count() == 1)
            {
                var _value = GetCumulative(iph.li_params, "+");
                _opStack.PushAt(iph.NextTuplePush, _value);
            }
        }
        private void CalculateAvg()
        {
            if (iph.li_paramsLoc.Count() == 1)
            {
                var _value = GetCumulative(iph.li_params, "+") / GetCumulative(iph.li_params, "count");
                _opStack.PushAt(iph.NextTuplePush, _value);
            }
        }

        // OUTPUT MULTI-VALUE
        private void CalculateAsc()
        {
            // TAKE SEQUENCES HERE, ERRORS ARE CAUGHT UPSTREAM
            if (iph.li_paramsLoc.Count() == 1)
            {

                iph.li_params.Sort();
                iph.li_params.Reverse();
                var _iterator = 0;
                foreach (var item in iph.li_params)
                {
                    _opStack.PushAt(iph.NextTuplePush - _iterator, item);
                    _iterator++;
                }
            }


        }
        private void CalculateDesc()
        {
            // TAKE SEQUENCES HERE, ERRORS ARE CAUGHT UPSTREAM
            if (iph.li_paramsLoc.Count() == 1)
            {

                iph.li_params.Sort();
                var _iterator = 0;
                foreach (var item in iph.li_params)
                {
                    _opStack.PushAt(iph.NextTuplePush - _iterator, item);
                    _iterator++;
                }
            }
        }
        private void CalculateSeq()
        {
            if (iph.li_paramsLoc.Count() == 1)
            {
                var _value = iph.li_params[0];
                PushSeq(iph.NextTuplePush, Convert.ToInt32(_value));
            }
        }
        private void CalculateDistinct()
        {
            if (iph.li_paramsLoc.Count() == 1)
            {
                PushDistinct(iph.NextTuplePush, iph.li_params);
            }
        }
        private void CalculateRunningTotal()
        {
            var _runningsum_data = ReturnParameter(1);
            _runningsum_data.Reverse();
            List<double> running_list = new List<double>();

            double total = 0;
            foreach (var item in _runningsum_data)
            {
                total += item;
                running_list.Add(total);
            }
            running_list.Reverse();
            PushListToStack(running_list);
        }

        #endregion

        #region function_input_type_MultiParam
        private void CalculateProduct()
        {
            if (iph.li_paramsLoc.Count() == 2)
            {
                var param1 = ReturnParameter(1);
                var param2 = ReturnParameter(2);
                PushProduct(param1, param2, iph.NextTuplePush);
            }
        }
        private void CalculateIIF()
        {
            var _iif_expr_lhs = ReturnParameter(1);
            var _iif_op = ReturnParameter(2);
            var _iif_expr_rhs = ReturnParameter(3);
            var _iif_T = ReturnParameter(4);
            var _iif_F = ReturnParameter(5);

            if (Convert.ToInt32(_iif_op[0]) == 0)//=
            {
                if (_iif_expr_lhs[0] == _iif_expr_rhs[0])
                {
                    PushListToStack(_iif_T);
                }
                else
                {
                    PushListToStack(_iif_F);
                }
            }

            if (Convert.ToInt32(_iif_op[0]) == -1)//<
            {
                if (_iif_expr_lhs[0] < _iif_expr_rhs[0])
                {
                    PushListToStack(_iif_T);
                }
                else
                {
                    PushListToStack(_iif_F);
                }
            }

            if (Convert.ToInt32(_iif_op[0]) == 1)//>
            {
                if (_iif_expr_lhs[0] > _iif_expr_rhs[0])
                {
                    PushListToStack(_iif_T);
                }
                else
                {
                    PushListToStack(_iif_F);
                }
            }


        }
        private void CalculateRound()
        {
            if (iph.li_paramsLoc.Count() == 2)
            {
                var round_param2 = ReturnParameter(2)[0];
                var round_param1 = ReturnParameter(1)[0];
                
                var _value = Math.Round(round_param1, Convert.ToInt32(round_param2));
                _opStack.PushAt(iph.NextTuplePush, _value);
            }
        }
        private void CalculatePower()
        {
            if (iph.li_paramsLoc.Count() == 2)
            {
                var power_param2 = ReturnParameter(2)[0];
                var power_param1 = ReturnParameter(1)[0];
                var _value = Math.Pow(power_param1, power_param2);
                _opStack.PushAt(iph.NextTuplePush, _value);
            }
        }
        private void CalculateLeft()
        {
            var _param1 = ReturnParameter(1);
            var _param2 = ReturnParameter(2)[0];
            PushTrim(_param1, iph.NextTuplePush, Convert.ToInt32(_param2), "left");
        }
        private void CalculateRight()
        {
            var _param1 = ReturnParameter(1);
            var _param2 = ReturnParameter(2)[0];
            PushTrim(_param1, iph.NextTuplePush, Convert.ToInt32(_param2), "right");
        }
        private void CalculateTake()
        {
            var _param1 = ReturnParameter(1);
            var _param2 = Convert.ToInt32(ReturnParameter(2)[0]);
            var _param3 = Convert.ToInt32(ReturnParameter(3)[0]);
           
            PushTake(iph.NextTuplePush, _param1, _param2, _param3);
        }
        #endregion

        #region function_input_type_Paramless
        private void CalculatePi()
        {
            if (iph.li_params.Count() == 0)
            {                
                _opStack.PushAt(iph.NextTuplePush, Math.PI);
            }
        }
        private void CalculateEuler()
        {
            if (iph.li_params.Count() == 0)
            {
                _opStack.PushAt(iph.NextTuplePush, Math.E);
            }
        }

        #endregion

        #endregion


        #region Special Functions
        public void PushProduct(List<double> _params1, List<double> _params2, int index)
        {
            // need to add a check for param1 & param2 being same count
            int _iterator = 0;
            foreach (var item in _params1)
            {
                var _value = item * _params2[_iterator];
                _opStack.PushAt(index - _iterator, _value);
                _iterator++;
            }

        }

        public void PushTrim(List<double> _params, int index, int splitAt, string function )
        {
            int _iterator = 0;
            int _index = index;
            int remove = _params.Count() - splitAt;
            if (function == "left")
            {
                #region remove
                for (int i = 0; i < remove; i++)
                {
                    _params.RemoveAt(0);
                }

                #endregion
            }
            
            do
            {
                var _value = _params[_iterator];
                _opStack.PushAt(_index - _iterator, _value);
                _iterator++;
            } while (_iterator < splitAt);

        }

        public void PushSeq( int index, int stopPoint)
        {

            _opStack.PushRipple(index, stopPoint);

            stopPoint = stopPoint +1;
            for (int i = 1;  i < stopPoint; i++)
            {
                var _value = stopPoint - i;
                _opStack.PushAt((stopPoint + index) - i, _value);              
            }

            _opStack.Order();// we have to do this when it's followed by a PeekSearchKeyAbove. We need to remove reliance on the ordering for PeekSearchKeyAbove to work.
        }

        public void PushTake(int index, List<double> _params,  int comparison_op, int comparison_value)
        {
            

           foreach(var item in _params)
            {
                if (comparison_op == -1)
                {
                    if (item < comparison_value)
                    {
                        _opStack.PushAt(index, item);
                        index--;
                    }
                }

                if (comparison_op == 0)
                {
                    if (item == comparison_value)
                    {
                        _opStack.PushAt(index, item);
                        index--;
                    }
                }

                if (comparison_op == 1)
                {
                    if (item > comparison_value)
                    {
                        _opStack.PushAt(index, item);
                        index--;
                    }
                }
            }


        }

        public void PushDistinct(int index, List<double> _params)
        {
            int _iterator = 0;
 
            _params = _params.Select(x => x).Distinct().OrderByDescending(x => x).ToList();
            do
            {
                var _value = _params[_iterator];
                _opStack.PushAt(index - _iterator, _value);
                _iterator++;
            } while (_iterator < _params.Count());
        }

        

       

        private static string DoubleToFraction(double num, double epsilon = 0.0001, int maxIterations = 20)
        {
            double[] d = new double[maxIterations + 2];
            d[1] = 1;
            double z = num;
            double n = 1;
            int t = 1;

            int wholeNumberPart = (int)num;
            double decimalNumberPart = num - Convert.ToDouble(wholeNumberPart);

            while (t < maxIterations && Math.Abs(n / d[t] - num) > epsilon)
            {
                t++;
                z = 1 / (z - (int)z);
                d[t] = d[t - 1] * (int)z + d[t - 2];
                n = (int)(decimalNumberPart * d[t] + 0.5);
            }

            return string.Format((wholeNumberPart > 0 ? wholeNumberPart.ToString() + " " : "") + "{0}/{1}",
                                 n.ToString(),
                                 d[t].ToString()
                                );
        }

        private List<double> ReturnParameter(int paramnum)
        {
            
            var which_param = paramnum ;
            var which_param_loc = which_param-1;

            var our_param_start_loc = iph.li_paramsLoc[which_param_loc];
            var our_param_end_loc = which_param== 1? iph.li_params.Count()-1 :   iph.li_paramsLoc[which_param_loc-1]-1;

            var offset = our_param_end_loc - our_param_start_loc+1;
            var _param = iph.li_params.GetRange(our_param_start_loc, offset);
            return _param;
           
        }

       

        private void PushListToStack(List<double> _param)
        {
            var _iterator = 0;
            foreach (var item in _param)
            {
                _opStack.PushAt(iph.NextTuplePush - _iterator, item);
                _iterator++;
            }
        }

        #endregion


    }







}
