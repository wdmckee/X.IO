using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Word
{
    public class WordParser
    {

        // the "Parser" for the folder always contains the top-most parent element
        public word word;



        private IList<SpecialToken> _tokens;
        private int _index;


        public WordParser(IList<SpecialToken> tokens, ref int index)
        {

            _tokens = tokens;
            _index = index;
            word = Parse_word();
            index = _index;

        }


        public word Parse_word()
        {
            /*word:   
                letter-sequence
                  */


            word _self = null;

            

           
            var _letter_sequence = Parse_letter_sequence();
            if (_letter_sequence != null )
            {
                _self = new word(_letter_sequence);

                return _self;
            }

            
            return _self;
        }


        #region RECURSIVES
        private letter_sequence Parse_letter_sequence(letter_sequence _self = null)
        {
            /*  letter-sequence:
                  letter
                | letter letter-sequence  */



            var _letter = Parse_letter();
            if (_letter != null)
            {
                var _letter_sequence = new letter_sequence(_letter, _self);
                _self = Parse_letter_sequence(_letter_sequence);
            }


            return _self;
        }
        #endregion


        #region ATOMS
        private letter Parse_letter()
        {
            if (_index >= _tokens.Count()) { return null; } // must be first line on any atom


            /* letter:
                    "A-Za-z" */


            var _data = _tokens[_index];
            var _result = new letter(_data, _index);



            if (_result.is_letter)
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
