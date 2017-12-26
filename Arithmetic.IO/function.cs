
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;

namespace X.IO.Arithmetic
{
    public class function
    {

        Helpers.Functions extern_fn = new Helpers.Functions();
        public string expression { get; }
        public Common.Word.word word { get; set; }


        public function(Common.Word.word _word)
        {
            var _exp = _word.expression.ToLower();
            if (
                   _exp == "abs"        //abs(#)
                || _exp == "frac"       //frac(#)
                || _exp == "sin"        //sin(#)
                || _exp == "cos"        //cos(#)
                || _exp == "tan"        //tan(#)
                || _exp == "exp"        //exp(#)
                || _exp == "log"        //log(#)
                || _exp == "multiply"   //multiply($)
                || _exp == "divide"     //divide($)
                || _exp == "sum"        //sum($)
                || _exp == "fibonacci"  //fibonacci(#)
                || _exp == "sqrt"       //sqrt(#)
                || _exp == "max"        //max($)
                || _exp == "min"        //min($)
                || _exp == "count"      //count($)
                || _exp == "avg"        //avg($)
                || _exp == "sturges"    //this will be replaces with lib fn
                || _exp == "round"      //round(<expr>|<decimals>)
                || _exp == "power"      //power(<expr>|<to>)
                || _exp == "product"    //product(<expr>|<expr>)
                || _exp == "left"       //
                || _exp == "right"      //
                || _exp == "seq"        //
                || _exp == "take"       //
                || _exp == "distinct"   //
                || _exp == "index"      //return the number at the specified index
                || _exp == "asc"        //
                || _exp == "desc"       //
                || _exp == "pi"         // pi()
                || _exp == "e"          // e()
                || _exp == "mod"        //
                || _exp == "iif"        //iif(#|=|#|T|F)
                || _exp == "colset"     //
                || _exp == "findindex"  //findindex(<expr>|<index_to_find>)returns the index(es) of the speified value
                || _exp == "nest"       //nest(<expr>|<expr>) nests the second param inside the first so 1,2,3|9,8,7 -> {{1,9},{2,8},{3,7}}
                || _exp == "runningtotal" //runningsum(<expr>) obvious
                || _exp == "frequency"
                || _exp == "mode"
                || _exp == "parameter"  //
                || _exp == "foreach"


                || extern_fn.ReturnFunctionName(_exp) ==true
                // ADD FUNCTION
                )
            {
                word = _word;
                expression = _word.expression;
            }

        }





    }
}
