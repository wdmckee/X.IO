
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Common.Word
{
    public class letter
    {

        public int index { get; }
        public string expression { get; }
        public bool is_letter { get; }
        public SpecialToken token { get; set; }
        public letter(SpecialToken data, int _index)
        {
            if (data.StringValue.Equals("a")
               || data.StringValue.Equals("b")
               || data.StringValue.Equals("c")
               || data.StringValue.Equals("d")
               || data.StringValue.Equals("e")
               || data.StringValue.Equals("f")
               || data.StringValue.Equals("g")
               || data.StringValue.Equals("h")
               || data.StringValue.Equals("i")
               || data.StringValue.Equals("j")
               || data.StringValue.Equals("k")
               || data.StringValue.Equals("l")
               || data.StringValue.Equals("m")
               || data.StringValue.Equals("n")
               || data.StringValue.Equals("o")
               || data.StringValue.Equals("p")
               || data.StringValue.Equals("q")
               || data.StringValue.Equals("r")
               || data.StringValue.Equals("s")
               || data.StringValue.Equals("t")
               || data.StringValue.Equals("u")
               || data.StringValue.Equals("v")
               || data.StringValue.Equals("w")
               || data.StringValue.Equals("x")
               || data.StringValue.Equals("y")
               || data.StringValue.Equals("z")

               || data.StringValue.Equals("A")
               || data.StringValue.Equals("B")
               || data.StringValue.Equals("C")
               || data.StringValue.Equals("D")
               || data.StringValue.Equals("E")
               || data.StringValue.Equals("F")
               || data.StringValue.Equals("G")
               || data.StringValue.Equals("H")
               || data.StringValue.Equals("I")
               || data.StringValue.Equals("J")
               || data.StringValue.Equals("K")
               || data.StringValue.Equals("L")
               || data.StringValue.Equals("M")
               || data.StringValue.Equals("N")
               || data.StringValue.Equals("O")
               || data.StringValue.Equals("P")
               || data.StringValue.Equals("Q")
               || data.StringValue.Equals("R")
               || data.StringValue.Equals("S")
               || data.StringValue.Equals("T")
               || data.StringValue.Equals("U")
               || data.StringValue.Equals("V")
               || data.StringValue.Equals("W")
               || data.StringValue.Equals("X")
               || data.StringValue.Equals("Y")
               || data.StringValue.Equals("Z")


               || data.StringValue.Equals("_")

                )
            {
                is_letter = true;
                token = data;
                expression = data.StringValue;
                index = _index;
            }
            else
            {
                is_letter = false;
            }




        }
    }
}
