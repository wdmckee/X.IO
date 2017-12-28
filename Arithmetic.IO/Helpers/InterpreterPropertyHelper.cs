using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Arithmetic.Helpers;
using X.IO.Common.Stack;

namespace Arithmetic.IO.Helpers
{

    /*
     This class helps the interpreter evaluate some of the more complex logic and sets flags
     to simplify the logic performed by the interpreter.
     It also pulls function parameters into an ordered list called li_params
     Associated with li_params is the li_paramsLoc list that identifies where paremeters exist inside fo the list


    We have 4 types of input operations
    (0) infix operators             -> +,-,*,/
	(1) paramless functions         -> pi(), e()
	(2) single value functions      -> abs(10), sum(1|2|3) the parameter-sequence is viewed as asingle param
	(3) parameterized functions     -> iif(1,>,2,1,0)  anything comma separated
	eventually we only want to combine #2 & #3 into #3 while removing the number_1 & number_2 vars


    We have 2 types of output operations
    (0) SingleValue
    (1) MultiValue

    */

    public class InterpreterPropertyHelper
    {



        public Dictionary<string, List<double>> _externalData = new Dictionary<string, List<double>>();
        public List<double> li_params = new List<double>();
        public List<int> li_paramsLoc = new List<int>();


        public bool isError;
        public double _number_1 { get; set; }
        public double _number_2 { get; set; }
        public string _current_operation { get; set; }
        public bool operation_done_last_round { get; set; }
        public OpStack _opStack { get; set; }
        public int NextTuplePush { get; set; }


        #region privates


        #region enums
        private enum function_input_type { SingleParam, MultiParam, Paramless, None }
        private function_input_type status_execute_function_input_type { get; set; }


        private enum function_output_type { SingleValue, MultiValue, None}
        private function_output_type status_execute_function_output_type { get; set; }
        #endregion




        // THESE 4 TELL US ABOUT OUR OPERATION TYPES
            // where do they start & end
        private int next_location_input_operation_operator_infix { get; set; }
        private int next_location_input_operation_function_singleValue { get; set; }
        private int next_location_input_operation_function_multiParam { get; set; }
        private int next_location_input_operation_function_paramless { get; set; }
        private int next_location_input_operation_function { get; set; }
        private int end_location_input_operation_function { get; set; }



        // THESE TELL US IF WE ARE DOING AN OPERATOR OR FUNCTION
        private bool status_execute_input_operation_function { get; set; }
        private bool status_execute_input_operation_operator { get; set; }

        
        private int next_array_end { get; set; }



        #endregion
        Functions intern_fn = new Functions();


        public InterpreterPropertyHelper(OpStack opStack, ref Dictionary<string, List<double>> externalData)
        {
            _opStack = opStack;
            _externalData = externalData;
        }



        public void StatusEvaluations()
        {

            

            // defaults
            status_execute_function_input_type = function_input_type.None;
            status_execute_function_output_type = function_output_type.None;
            operation_done_last_round = true;
            _number_2 = 0;
            _number_1 = 0;
            next_array_end = -1;

            
            
            next_location_input_operation_function_singleValue = _opStack.PeekSearchNext(intern_fn.function_input_type_SingleParam.ToArray());
            next_location_input_operation_function_multiParam = _opStack.PeekSearchNext(intern_fn.function_input_type_MultiParam.ToArray());           
            next_location_input_operation_function_paramless = _opStack.PeekSearchNext(intern_fn.function_input_type_Paramless.ToArray()); // _opStack.PeekValueAt(_opStack.PeekSearchKeyPrevious(next_location_input_operation_function)).ToString() == "end" ? next_location_input_operation_function : -1; // if the next key is just an end, we know its paramless

            next_location_input_operation_function = (next_location_input_operation_function_singleValue+ next_location_input_operation_function_multiParam+ next_location_input_operation_function_paramless)==-3?-1 :(new List<int> { next_location_input_operation_function_singleValue, next_location_input_operation_function_multiParam, next_location_input_operation_function_paramless }).Where(x=>x > -1).Min();   //Math.Min(next_location_input_operation_function_singleValue, next_location_input_operation_function_multiParam) == -1 ? Math.Max(next_location_input_operation_function_singleValue, next_location_input_operation_function_multiParam) : Math.Min(next_location_input_operation_function_singleValue, next_location_input_operation_function_multiParam);
            next_location_input_operation_operator_infix = _opStack.PeekSearchNext(intern_fn.operator_input_type_infix.ToArray());


            status_execute_input_operation_function = _opStack.FirstValue(next_location_input_operation_function, next_location_input_operation_operator_infix) == next_location_input_operation_function ? true : false;
            status_execute_input_operation_operator = status_execute_input_operation_function == false && next_location_input_operation_operator_infix > -1 ? true : false;

            // exit we are done if we have nor operators
            if (!status_execute_input_operation_function && !status_execute_input_operation_operator) { operation_done_last_round = false; return; }

            if(next_location_input_operation_function > -1)
            end_location_input_operation_function = _opStack.PeekSearchAbove(next_location_input_operation_function, "end");
            


         


            _current_operation = status_execute_input_operation_function==true? _opStack.PeekValueAt(next_location_input_operation_function) : _opStack.PeekValueAt(next_location_input_operation_operator_infix);
            
            if (status_execute_input_operation_function && intern_fn.function_input_type_SingleParam.Contains(_current_operation)) { status_execute_function_input_type = function_input_type.SingleParam; }
            if (status_execute_input_operation_function && intern_fn.function_input_type_MultiParam.Contains(_current_operation)) { status_execute_function_input_type = function_input_type.MultiParam; }
            if (status_execute_input_operation_function && intern_fn.function_input_type_Paramless.Contains(_current_operation)) { status_execute_function_input_type = function_input_type.Paramless; }

            if (intern_fn.function_output_type_MultiValue.Contains(_current_operation)) { status_execute_function_output_type = function_output_type.MultiValue; }
            if (intern_fn.function_output_type_SingleValue.Contains(_current_operation)) { status_execute_function_output_type = function_output_type.SingleValue; }

            NextTuplePush = status_execute_input_operation_function == true ? next_location_input_operation_function : next_location_input_operation_operator_infix;



            if (status_execute_input_operation_operator)
            {
                DoUnaryOperatorOperation();
            }

            if (status_execute_input_operation_function && status_execute_function_input_type == function_input_type.SingleParam)
            {
                DoSingleParamOperation();
            }

            if (status_execute_input_operation_function && status_execute_function_input_type == function_input_type.MultiParam)
            {
                DoMultiParamOperation();
            }

            if (status_execute_input_operation_function && status_execute_function_input_type == function_input_type.Paramless)
            {
                DoParamlessOperation();
            }

            if (!status_execute_input_operation_function && !status_execute_input_operation_operator)
            {
                operation_done_last_round = false;
            }



        }

       

        private void DoUnaryOperatorOperation()
        {          
                
                _number_2 = _opStack.PopAt(_opStack.PeekSearchKeyPrevious(next_location_input_operation_operator_infix));
                _number_1 = _opStack.PeekSearchKeyPrevious(next_location_input_operation_operator_infix) == -1 ? 0 : _opStack.PopAt(_opStack.PeekSearchKeyPrevious(next_location_input_operation_operator_infix));
                _current_operation = _opStack.PopAt(next_location_input_operation_operator_infix);
                NextTuplePush = next_location_input_operation_operator_infix;

                Console.WriteLine("{0} {1}  {2}  {3}", "step:", _number_1, _current_operation, _number_2);
        }

        private void DoSingleParamOperation()
        {
            // GET INDEX INTO THE PARAM (WHICH COULD BE AN ARRAY BUT STILL A SINGLE PARAM)
            var _next_value_location = _opStack.PeekSearchKeyPrevious(next_location_input_operation_function);
            li_paramsLoc.Add(0);
            do
            {                
                var _value =_opStack.PeekValueAt(_next_value_location);// HOLD OUR VALUE (DOUBLE)

                if (_value is string && _value == "|") //PER LANGUAGE SPECS, WE CAN HAVE MULTIPLE VALUES COMBINED BY A PIPE AND IS STILL VIEWED AS A SINGLE PARAM
                {
                    _opStack.PopAt(_next_value_location);
                }
                else if (_value is Int32 || _value is double || _value is Single)//PER LANGUAGE SPECS, THIS MUST BE AN INT OR DOUBLE VALUE
                {   
                    li_params.Add(_value);// ADD TO PARAM LIST
                    var index = _opStack.PeekSearchKeyPrevious(_next_value_location); // GET THE NEXT INDEX LOCATION
                    _opStack.PopAt(_next_value_location);//REMOVE DOUBLE VALUE FROM STACK
                    _next_value_location = index; //RESET TO INDEX
                }
                else if (_value is string && _value != "|" && _value != ",") // CHECKING FOR AN EXTERNAL ARRAY REFERENCE
                {
                    li_params.AddRange(_externalData[_value]);// DO ERROR HANDLE HERE
                    var index = _opStack.PeekSearchKeyPrevious(_next_value_location); // GET THE NEXT INDEX LOCATION
                    _opStack.PopAt(_next_value_location);//REMOVE DOUBLE VALUE FROM STACK
                    _next_value_location = index; //RESET TO INDEX
                }
                else
                {
                    var t = _value.GetType();
                    _opStack.Clear();
                    _opStack.Push("ERROR:1001");
                    isError = true;
                    return;

                    //throw new Exception(string.Format("dude, yo! you can only have a number or a number sequence here. I can't interpret this mess --> \"{0}\".",_value));
                }
                
                

            } while (_next_value_location > end_location_input_operation_function);

            //NOW THAT OUR LOOP IS DONE, LETS REMOVE THE FUNCTION IDENTIFIER AND ITS CORRESPONDING END FLAG
           
                _opStack.PopAt(next_location_input_operation_function);//REMOVE THE IDENTIFIER
                _opStack.PopAt(_next_value_location);//REMOVE THE END FLAG

                Console.WriteLine("{0} {1}", "step:",  _current_operation);
        }
              
        private void DoMultiParamOperation()
        {
            // GET INDEX INTO THE PARAM (WHICH COULD BE AN ARRAY BUT STILL A SINGLE PARAM)
            var _next_value_location = _opStack.PeekSearchKeyPrevious(next_location_input_operation_function);
            li_paramsLoc.Add(0);
            do
            {
                var _value = _opStack.PeekValueAt(_next_value_location);// HOLD OUR VALUE (DOUBLE)

                if (_value is string && _value == "|") //PER LANGUAGE SPECS, WE CAN HAVE MULTIPLE VALUES COMBINED BY A PIPE AND IS STILL VIEWED AS A SINGLE PARAM
                {
                    _opStack.PopAt(_next_value_location);
                }               
                else if (_value is Int32 || _value is double || _value is Single || _value is string)//PER LANGUAGE SPECS, THIS MUST BE AN INT OR DOUBLE VALUE
                {
                    // EXPLANATION:
                    // IF WE HAVE A STRING ITS 1 or 2 THINGS... a RESERVED CHAR LIKE <>=@ OR A [COLUMNNAME]
                    if (_value is string) 
                    {
                        _value = TranslateSpecialCharToIntCode(_value);

                        if (_value == -99) // CHECKING FOR AN EXTERNAL ARRAY REFERENCE WHICH IS CODED -99
                        {
                            _value = _opStack.PeekValueAt(_next_value_location);
                            li_params.AddRange(_externalData[_value]); // ADD RANGE SINCE WE MAY ALREADY HAVE PARAMS IN THE LIST
                        }
                        else
                        {
                            li_params.Add(_value);// ADD TO PARAM LIST IF IT WAS REALLY A SPECIAL CHAR
                        }
                     }
                    else
                    {
                        li_params.Add(_value);// ADD TO PARAM LIST IF IT WAS NOT A COLUMNNAME OR SPECIAL CHAR (INT,DOUBLE SINGLE)
                    }

                    


                    var index = _opStack.PeekSearchKeyPrevious(_next_value_location); // GET THE NEXT INDEX LOCATION
                    _opStack.PopAt(_next_value_location);//REMOVE DOUBLE VALUE FROM STACK
                    _next_value_location = index; //RESET TO INDEX
                }
               
                else
                {                    
                    _opStack.Clear();
                    _opStack.Push("ERROR:1001");
                    isError = true;
                    return;

                    //throw new Exception(string.Format("dude, yo! you can only have a number or a number sequence here. I can't interpret this mess --> \"{0}\".",_value));
                }

                var temp = _opStack.PeekValueAt(_next_value_location);
                if (temp  is string && temp == ",")
                {
                    var index = _opStack.PeekSearchKeyPrevious(_next_value_location);// GET THE NEXT INDEX LOCATION
                    _opStack.PopAt(_next_value_location);// remove the comma
                    li_paramsLoc.Add(li_params.Count());
                    _next_value_location = index; //RESET TO INDEX
                }

            } while (_next_value_location > end_location_input_operation_function);

            //NOW THAT OUR LOOP IS DONE, LETS REMOVE THE FUNCTION IDENTIFIER AND ITS CORRESPONDING END FLAG

            _opStack.PopAt(next_location_input_operation_function);//REMOVE THE IDENTIFIER
            _opStack.PopAt(_next_value_location);//REMOVE THE END FLAG    


            li_paramsLoc.Reverse();
            Console.WriteLine("{0} {1}", "step:", _current_operation);
        }

       

        private void DoParamlessOperation()
        {

            _opStack.PopAt(next_location_input_operation_function_paramless);
            _opStack.PopAt(end_location_input_operation_function);
            Console.WriteLine("{0} {1}", "step:", _current_operation);
        }







        private int TranslateSpecialCharToIntCode(string v)
        {
            int _value =0;
            switch (v)
            {
                case "<":
                    _value = - 1;
                    break;
                case "=":
                    _value = 0;
                    break;
                case ">":
                    _value = 1;
                    break;
                case "@":
                    _value = 0;
                    break;
                default:
                    _value = -99;
                    break;
            }

            return _value;
        }





    }
}
