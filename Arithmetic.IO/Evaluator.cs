using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.IO.Common;
using X.IO.Common.Stack;
namespace X.IO.Arithmetic
{
    public class Evaluator
    {


        public Result Eval(string expr)
        {
           return  Eval(expr, null);
        }


        public Result Eval(string expr, Dictionary<string, dynamic> external_data = null)
        {


            if (expr == "?") { return new Result(help()); }

            
            int _index = 0;


            ArithmeticScanner scanner = new ArithmeticScanner();
            var tokens = scanner.Scan(expr);

            ArithmeticParser p = new ArithmeticParser(tokens, ref _index);
            if (p.result.IsError == true) {  p.result.value = ""; return p.result; }           
           
            ArithmeticInterpreter i = new ArithmeticInterpreter(external_data);
            i.Result(p.result);
            p.result.value = i.ActionList[0].event_data;

            if(i.IsError){p.result.IsError = true;}

            return p.result;
                
            }


        
        private string help()
        {

            var value = @"

SINGLE-VALUE OUTPUT
abs(<expr>) returns {absolute value of <expr>}
frac(<expr>) returns {fractional equivalent of <expr>}
sin(<expr>) returns {sin of <expr>}
cos(<expr>) returns {cos of <expr>}
tan(<expr>) returns {tan of <expr>}
exp(<expr>) returns {exp of <expr>}
log(<expr>) returns {log of <expr>}
sqrt(<expr>) returns {sqrt of <expr>}
fibonacci(<expr>)  returns {nth element in the fibonacci seq where <expr> is the nth element}
pi() returns {pi}
e() returns {e}
max(<expr-sequence>) returns {largest number in <expr-sequence>}
min(<expr-sequence>) returns {smallest number in <expr-sequence>}
count(<expr-sequence>) returns {count of numbers in <expr-sequence>}
sum(<expr-sequence>) returns {sum of numbers in <expr-sequence>}
avg(<expr-sequence>) returns {avg of numbers in <expr-sequence>}
mod(<[dividend]expr>,<[divisor]expr>) returns {remainder}
round(<expr>,<[decimals]expr>) returns {<expr> rounded to <[decimals]expr> places}
power(<expr>,<[power]expr>) returns {<expr> raised to <[power]expr> power}
left(<expr-sequence>,<[nth]expr>) returns {<expr-sequence> up to the <[nth]expr> element by ordinal position from left to right}
right(<expr-sequence>,<[nth]expr>) returns {<expr-sequence> up to the <[nth]expr> element by ordinal position from right to left}
index(<expr-sequence>,<expr>) returns {ordinal postion of the <expr-sequence> where <expr> can be found, 0 if not found}


MULTI-VALUE OUTPUT
asc(<expr-sequence>) returns {<expr-sequence> in ascending order}
desc(<expr-sequence>) returns {<expr-sequence> in descending order}
seq(<expr>) returns {genreates a <expr-sequence> of numbers up to and equal to <expr>}
distinct(<expr-sequence>) returns {unique elements of <expr-sequence>}
product(<expr-sequence>,<expr-sequence>) returns {<expr-sequence> where both params ar multiplied by each other}
iif(<expr>,<op>,<[comparison]expr>,<[T]expr>,<[F]expr>)  returns {<[T]expr> or <[F]expr> using <,>,= as comparison operators}
take(<expr-sequence>,<op>,<[comparison]expr>) returns {<expr-sequence> using <,>,= as comparison operators}
runningtotal(<expr-sequence>) returns {running total of <expr-sequence>}


";

            return value;
            
        }



       


    }
}
