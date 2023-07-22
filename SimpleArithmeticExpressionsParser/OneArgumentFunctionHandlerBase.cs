using System;

namespace SimpleArithmeticExpressionsParser
{
    public abstract class OneArgumentFunctionHandlerBase : IFunctionHandler
    {
        public string Handle(string expression)
        {
            var parser = new Parser();
            
            while (expression.Contains(FunctionName))
            {
                var index = expression.IndexOf(FunctionName, StringComparison.Ordinal);
                var to = BracketsHelper.SkipBrackets(index + FunctionName.Length, expression);
                var withoutFunc = expression
                    .Substring(index + FunctionName.Length, to - index - FunctionName.Length);
                parser.Expression = withoutFunc;
                var result = ApplyFunction(parser.CalculateResult());
                expression = expression.Replace(FunctionName + withoutFunc, result.ToString());
            }

            return expression;
        }

        protected abstract string FunctionName { get; }
        
        protected abstract double ApplyFunction(double value);
    }
}