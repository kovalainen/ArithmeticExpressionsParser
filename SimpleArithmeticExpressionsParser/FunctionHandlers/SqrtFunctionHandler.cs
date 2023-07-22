using System;

namespace SimpleArithmeticExpressionsParser.FunctionHandlers
{
    public class SqrtFunctionHandler : OneArgumentFunctionHandlerBase
    {
        protected override string FunctionName => nameof(Math.Sqrt).ToLower();
        
        protected override double ApplyFunction(double value) => Math.Sqrt(value);
    }
}