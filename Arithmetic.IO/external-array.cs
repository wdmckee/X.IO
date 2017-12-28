using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.IO.Arithmetic
{
    public class external_array
    {

        public string expression { get; }
        public X.IO.Common.Word.word word { get; set; }
        public bracket ident_lhs { get; set; }
        public bracket ident_rhs { get; set; }



        public external_array( bracket _ident_lhs, X.IO.Common.Word.word _word,  bracket _ident_rhs)
        {
            ident_lhs = _ident_lhs;
            word = _word;
            ident_rhs = _ident_rhs;

            expression = ident_lhs.expression + word.expression + ident_rhs.expression;
        }






    }
}
