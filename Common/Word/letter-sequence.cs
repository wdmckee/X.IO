using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Word
{
    public class letter_sequence
    {

        public int begin_index { get; }
        public int end_index { get; }
        public string expression { get; }
        public letter letter { get; set; }
        public letter_sequence self_letter_sequence { get; set; }

        public letter_sequence(letter _letter)
        {
            letter = _letter;
            expression = letter.expression;
            begin_index = _letter.index;
            end_index = _letter.index;
        }

        public letter_sequence(letter _letter, letter_sequence _letter_sequence)
        {
            letter = _letter;
            self_letter_sequence = _letter_sequence;
            expression = self_letter_sequence?.expression + letter.expression;
            begin_index = self_letter_sequence == null ? letter.index : self_letter_sequence.begin_index;
            end_index = letter.index;
        }




    }
}
