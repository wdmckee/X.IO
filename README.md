# X.IO
```
A parsing library consisting of several projects.

Current Projects:
  Arithmetic.IO
PLanned Projects:
  String.IO
  ControlFlow.IO


```
# ARITHMETIC.IO

```

using X.IO.Arithmetic;
Evaluator expr = new Evaluator();
var result = expr.Eval("round(1/6*(tan(sin(-6+2))*cos(.25))+log(10)-exp(7)+8.88E-11-100.0,4)");
Console.WriteLine("expression: {0}",result.expression);
Console.WriteLine("answer: {0}", result.value);
Console.WriteLine("any errors? {0}", result.IsError);
Console.Read();






arith-expression:
    term
    | term term-sequence
term-sequence:
	infix-operator-type1 term
	| infix-operator-type1 term term-sequence	
term:
	factor
    | factor factor-sequence
factor-sequence:
	infix-operator-type2 factor
	| infix-operator-type2 factor factor-sequence
arith-expression-sequence: 
	 external-array
	| arith-expression
	| arith-expression pipe arith-expression-sequence	 
parameter-sequence:
	arith-expression-sequence
	| arith-expression-sequence comma parameter-sequence
function-parameter:
	minus? paren? parameter-sequence? paren?
	| array-function   *** NOT IMPLEMENTED ***  
factor:
	Number.number 	
	| iterator
	| conditional   
	| signed-function? function-parameter
array-function:
	( "{" parameter-sequence comma arith-expression "}" )


external-array:
	bracket word bracket  (lookup the System.Array[dynamic])
signed-function:
	minus? function
function:
	Word.word
infix-operator-type1:
	"+" | "-" 
infix-operator-type2:
	"*" | "/" 
minus:
	"+" | "-"
iterator:
	"@"
paren:
	"(" | ")"
bracket:
	"[" | "]"
pipe:
	"|"
conditional:
	">" | "<" | "="





ERROR:1000 | looks like you tried to feed a parameter sequence (array) to a function that only accepts a single number as a parameter | scope:interpreter.calculations
ERROR:1001 | only number sequences (arrays) are allowed in this context	| scope:interpreter.evaluations
ERROR:1002 | not correct number of params	| scope:interpreter.calculations