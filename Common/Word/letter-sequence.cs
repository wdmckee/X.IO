using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Word
{
    public class letter_sequence
    {


        public string expression { get; }
        public letter letter { get; set; }
        public letter_sequence self_letter_sequence { get; set; }

        public letter_sequence(letter _letter)
        {
            letter = _letter;
            expression = letter.expression;
        }

        public letter_sequence(letter _letter, letter_sequence _letter_sequence)
        {
            letter = _letter;
            self_letter_sequence = _letter_sequence;
            expression = self_letter_sequence?.expression + letter.expression;
        }




    }
}
