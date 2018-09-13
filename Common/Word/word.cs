using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Word
{
    public class word
    {

        public int begin_index { get; }
        public int end_index { get; }
        public string expression { get; }
        public letter_sequence letter_sequence {get; set;}

        public word(letter_sequence _letter_sequence)
        {
            letter_sequence = _letter_sequence;
            expression = letter_sequence.expression;
            begin_index = _letter_sequence.begin_index;
            end_index = _letter_sequence.end_index;
        }

    }
}
