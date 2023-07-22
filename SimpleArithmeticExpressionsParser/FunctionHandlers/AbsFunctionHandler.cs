using System;

namespace SimpleArithmeticExpressionsParser.FunctionHandlers
{
    public class AbsFunctionHandler : OneArgumentFunctionHandlerBase
    {
        protected override string FunctionName => nameof(Math.Abs).ToLower();
        
        protected override double ApplyFunction(double value) => Math.Abs(value);
    }
}