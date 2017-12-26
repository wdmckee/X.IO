
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Number
{
    public class NumberParser
    {



        // the "Parser" for the folder always contains the top-most parent element
        public number number;



        private IList<SpecialToken> _tokens;
        private int _index;


        public NumberParser(IList<SpecialToken> tokens, ref int index)
        {
            _tokens = tokens;
            _index = index;
            number = Parse_number();
            index = _index;
        }

        //-----------------------------------------------------------

        public  number Parse_number()
        {
            /*number:   
                sign? integer-constant
                | sign? floating-point-constant  */
            var _backtrackindex_1 = _index;

            number _self = null;


            List<sign> __sign = new List<sign>();
            var _sign = Parse_sign();
            while (_sign != null) { __sign.Add(_sign); _sign = Parse_sign(); }
            _sign = new sign(__sign);
            //var _sign = Parse_sign();

            var _backtrackindex_2 = _index;
            var _integer_constant = Parse_integer_constant();
            if (_integer_constant != null && ((_index < _tokens.Count() && _tokens[_index].StringValue != ".") || _index == _tokens.Count()))
            {
                _self = new number(_sign, _integer_constant);

                return _self;
            }

            if (_index < _tokens.Count()) { _index = _backtrackindex_2; } else { return null; }

            var _floating_point_constant = Parse_floating_point_constant();
            if (_floating_point_constant != null)
            {
                _self = new number(_sign, _floating_point_constant);

                return _self;
            }


            if (_self ==  null) { _index = _backtrackindex_1; } else { return null; }
            return _self;
        }


        private integer_constant Parse_integer_constant()
        {
            /* integer-constant:
                    digit-sequence */


            integer_constant _self = null;

            var _digit_sequence = Parse_digit_sequence();

            if (_digit_sequence != null)
            {
                _self = new integer_constant(_digit_sequence);

                return _self;
            }

            return _self;
        }

        private floating_point_constant Parse_floating_point_constant()
        {
            /*  floating-point-constant:
                    fractional-constant exponent?
                    | digit-sequence exponent*/


            floating_point_constant _self = null;

            var _fractional_constant = Parse_fractional_constant();
            if (_fractional_constant != null)
            {
                var _exp = Parse_exponent();
                _self = new floating_point_constant(_fractional_constant, _exp);

                return _self;
            }

            var _digit_sequence = Parse_digit_sequence();
            if (_digit_sequence != null)
            {
                var _exp = Parse_exponent();
                _self = new floating_point_constant(_digit_sequence, _exp);

                return _self;
            }




            return _self;
        }


        private fractional_constant Parse_fractional_constant()
        {
            /* fractional-constant:
                    digit-sequence? "." digit-sequence
                    | digit-sequence "." */



            fractional_constant _self = null;


            var _lhs_digit_sequence = Parse_digit_sequence();

            var _ident = _tokens[_index]; if (_tokens[_index].StringValue == ".") { _index++; }   //derek          

            var _rhs_digit_sequence = Parse_digit_sequence();

            if (_rhs_digit_sequence != null)
            {
                _self = new fractional_constant(_rhs_digit_sequence, _ident, _lhs_digit_sequence);

                return _self;

            }


            if (_rhs_digit_sequence == null)
            {
                _ident = _tokens[_index]; if (_tokens[_index].StringValue == ".") { _index++; } else { return null; }

                _self = new fractional_constant(_lhs_digit_sequence, _ident);
                return _self;
            }




            return _self;
        }

        private exponent Parse_exponent()
        {
            /*                         
                exponent:
                    ( "e" | "E" ) sign? digit-sequence
                */

            if (_index >= _tokens.Count()) { return null; }

            exponent _self = null;

            var _ident = _tokens[_index]; if (_tokens[_index].StringValue == "E" || _tokens[_index].StringValue == "e") { _index++; } else {; return null; }

            var _sign = Parse_sign();

            var _digit_sequence = Parse_digit_sequence();



            if (_ident.StringValue == "E" && _digit_sequence != null)
            {
                _self = new exponent(_ident, _digit_sequence, _sign);

                return _self;
            }




            return _self;
        }


        #region RECURSIVES
        private digit_sequence Parse_digit_sequence(digit_sequence _self = null)
        {
            /*  digit-sequence:
                  digit
                | digit digit-sequence  */



            var _digit = Parse_digit();
            if (_digit != null)
            {
                var _digit_sequence = new digit_sequence(_digit, _self);
                _self = Parse_digit_sequence(_digit_sequence);
            }


            return _self;
        }

        #endregion

        #region ATOMS

        private sign Parse_sign()
        {
            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom

            /*sign:
                "+" | "-" */

            var _data = _tokens[_index];
            var _result = new sign(_data);



            if (_result.is_sign)
            {
                _index++;
                return _result;
            }
            else
            {
                return null;
            }

        }

        private digit Parse_digit()
        {
            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom


            /* digit:
                "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9" */


            var _data = _tokens[_index];
            var _result = new digit(_data);



            if (_result.is_digit)
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
