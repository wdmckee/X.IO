using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Word
{
    public class word
    {

        public string expression { get; }
        public letter_sequence letter_sequence {get; set;}

        public word(letter_sequence _letter_sequence)
        {
            letter_sequence = _letter_sequence;
            expression = letter_sequence.expression;
        }

    }
}
