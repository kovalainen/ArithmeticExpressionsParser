using System;

namespace SimpleArithmeticExpressionsParser.FunctionHandlers
{
    public class SinFunctionHandler : OneArgumentFunctionHandlerBase
    {
        protected override string FunctionName => nameof(Math.Sin).ToLower();
        
        protected override double ApplyFunction(double value) => Math.Sin(value);
    }
}