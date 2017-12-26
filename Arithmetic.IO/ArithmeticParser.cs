using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;
using X.IO.Common.Number;
using X.IO.Common.Word;
using X.IO.Common.Utility;
using X.IO.Common.Stack;


namespace X.IO.Arithmetic
{
    public class ArithmeticParser
    {

        // the "Parser" for the folder always contains the top-most parent element
        public arith_expression arith_expression;
        public Result result; 


        private IList<SpecialToken> _tokens;
        private int _index;


        public ArithmeticParser(IList<SpecialToken> tokens, ref int index)
        {
            _tokens = tokens;
            _index = index;
            arith_expression = Parse_arith_expression();
            index = _index;
            result = new Result(arith_expression);
        }

        #region STANDARD

        private arith_expression Parse_arith_expression()
        {
           
            arith_expression _self = null;


            var _term = Parse_term();

            if (_term != null)
            {                
                _self = new arith_expression(_term);
            }


            var _term_sequence = Parse_term_sequence();
            if (_term != null && _term_sequence != null)
            {
                _self = new arith_expression(_term, _term_sequence);
            }




            return _self;

            

        }
                
        private term Parse_term()
        {
            

            term _self = null;
            
            var _factor = Parse_factor();


            if (_factor != null)
            {
                _self = new term(_factor);
            }

            var _factor_sequence = Parse_factor_sequence();
            if (_factor != null && _factor_sequence != null)
            {                               
                _self = new term(_factor, _factor_sequence);
            }

             return _self;
        }
        
        private factor Parse_factor()
        {

           
            if (_index >= _tokens.Count()) { return null; }
            

            factor _self = null;
            
            var _number = new NumberParser(_tokens, ref _index);// this picks up negative numbers but.....
            if (_number.number != null)
            {
                _self = new factor(_number.number); return _self;
            }

            var _iterator = Parse_iterator();// this picks up negative numbers but.....
            if (_iterator != null)
            {
                _self = new factor(_iterator); return _self;
            }


            var _conditional = Parse_conditional();
            if (_conditional != null)
            {
                _self = new factor(_conditional); return _self;

            }


            var _function = Parse_signed_function();           
            var _function_parameter = Parse_function_parameter();
            if (_function_parameter != null)
            {
                _self = new factor(_function, _function_parameter);
                return _self;
            }

        

            return _self;
           
           
        }      
        
        private function_parameter Parse_function_parameter()
        {
                                    
            function_parameter _self = null;

            var _minus = Parse_minus();            
            
            var _ident_lhs = Parse_Paren();

            var _ident_rhs = Parse_Paren();if (_ident_rhs != null && _ident_rhs.expression == "(") { _ident_rhs = null; _index--; }
            // this allows us to have functions with no parameters by checking for back to back of opposite direction.

            if (_ident_rhs == null)
            {
                var _parameter_sequence = Parse_parameter_sequence();
                _ident_rhs = Parse_Paren();
                _self = new function_parameter(_minus, _ident_lhs, _parameter_sequence, _ident_rhs);
            }
            else
            {
                _self = new function_parameter(_minus, _ident_lhs, null, _ident_rhs);
            }
            
                      
            return _self;
        }
        
        public signed_function Parse_signed_function()
        {
            signed_function _self = null;

            var _backtrack = _index;
            minus _minus = null;
            _minus = Parse_minus();

            var _function = Parse_function();

            if (_function == null) { _index = _backtrack; return null; }

            _self = new signed_function(_minus, _function);
            return _self;

        }

        private function Parse_function()
        {
            if (_index >= _tokens.Count()) { return null; }
            

            function _self = null;
            var _word = new WordParser(_tokens, ref _index);
            if (_word.word != null)
            {
                _self = new function(_word.word);
            }

             return _self;
        }

        #endregion
        

        #region RECURSIVES
        private term_sequence Parse_term_sequence(term_sequence _self = null)
        {
            

            
            var _infix_operator_type1 = Parse_infixOperator_type1();
            if (_infix_operator_type1 == null)
            {
                               return _self;
            }

            var _term = Parse_term();


            if (_term != null)
            {
                var _term_sequence = new term_sequence(_infix_operator_type1, _term, _self);
                _self = Parse_term_sequence(_term_sequence);
            }

            return _self;
        }
        private factor_sequence Parse_factor_sequence(factor_sequence _self = null)
        {
            /*

            factor-sequence:
	            infix-operator-type2 factor
	            | infix-operator-type2 factor factor-sequence

            */
            

            var _infix_operator_type2 = Parse_infixOperator_type2();
            if (_infix_operator_type2 == null)
            {
                                return _self;
            }

            var _factor = Parse_factor();

            if (_factor != null)
            {
                var _factor_sequence = new factor_sequence(_infix_operator_type2, _factor, _self);
                _self = Parse_factor_sequence(_factor_sequence);
            }


                        return _self;
        }

        public arith_expression_sequence Parse_arith_expression_sequence(arith_expression_sequence _self = null)
        {
            var _pipe = Parse_pipe();
            if (_self != null && _pipe == null) { return _self; }
            //  very important - stops infinite loop basically we must have a "|number" following

            var _arith_expression = Parse_arith_expression();

            if (_arith_expression != null && _pipe == null)
            {
                var _arith_expression_sequence = new arith_expression_sequence(_arith_expression);
                _self = Parse_arith_expression_sequence(_arith_expression_sequence);
                return _self;
            }

            if (_arith_expression != null && _pipe != null)
            {
                var _arith_expression_sequence = new arith_expression_sequence(_arith_expression,_self, _pipe);
                _self = Parse_arith_expression_sequence(_arith_expression_sequence);
                return _self;
            }

            return _self;


           
        }

        public parameter_sequence Parse_parameter_sequence(parameter_sequence _self = null)
        {
            var _comma = Parse_comma();
            if (_self != null && _comma == null) { return _self; }
            //  very important - stops infinite loop basically we must have a ",expr" following

            var _arith_expression_sequnce = Parse_arith_expression_sequence();

            if (_arith_expression_sequnce != null && _comma == null)
            {
                var _parameter_sequence = new parameter_sequence(_arith_expression_sequnce);
                _self = Parse_parameter_sequence(_parameter_sequence);
                return _self;
            }

            if (_arith_expression_sequnce != null && _comma != null)
            {
                var _parameter_sequence = new parameter_sequence(_arith_expression_sequnce, _self, _comma);
                _self = Parse_parameter_sequence(_parameter_sequence);
                return _self;
            }

            return _self;



        }

        #endregion


        #region ATOMS

        private infix_operator_type1 Parse_infixOperator_type1()
        {
            

            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom
            
            /*  infix_operator_type1:
                    ("+" | "-") */

            var _data = _tokens[_index];
            var _self = new infix_operator_type1(_data);



            if (_self.is_infix_operator_type1)
            {
                _index++;
                 return _self;
            }
            else
            {
                 return null;
            }


        }

        private infix_operator_type2 Parse_infixOperator_type2()
        {
            

            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom
            
            /*  infix_operator_type1:
                    ("*" | "/") */

            var _data = _tokens[_index];
            var _self = new infix_operator_type2(_data);



            if (_self.is_infix_operator_type2)
            {
                _index++;
                   return _self;
            }
            else
            {
                      return null;
            }


        }

        private paren Parse_Paren()
        {
            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom
            
            /*  infix_operator_type1:
                    ( ) */

            var _data = _tokens[_index];
            var _self = new paren(_data);



            if (_self.is_paren)
            {
                _index++;
                 return _self;
            }
            else
            {
                return null;
            }
        }

    

        private minus Parse_minus()
        {
            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom

            /*  "-" */

            var _data = _tokens[_index];
            var _self = new minus(_data);



            if (_self.is_minus)
            {
                _index++;
                return _self;
            }
            else
            {
                return null;
            }
        }

        private comma Parse_comma()
        {
            /* comma:
                "," */


            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom


            var _data = _tokens[_index];
            var _result = new comma(_data);


            if (_result.is_comma)
            {
                _index++;
                return _result;
            }
            else
            {
                return null;
            }
        }

        private pipe Parse_pipe()
        {
            /* comma:
                "," */


            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom


            var _data = _tokens[_index];
            var _result = new pipe(_data);


            if (_result.is_pipe)
            {
                _index++;
                return _result;
            }
            else
            {
                return null;
            }
        }


        private iterator Parse_iterator()
        {
            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom


            var _data = _tokens[_index];
            var _result = new iterator(_data);


            if (_result.is_iterator)
            {
                _index++;
                return _result;
            }
            else
            {
                return null;
            }
        }

        private conditional Parse_conditional()
        {
           


            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom


            var _data = _tokens[_index];
            var _result = new conditional(_data);


            if (_result.is_conditional)
            {
                _index++;
                return _result;
            }
            else
            {
                return null;
            }
        }



        #endregion
    }
}
