using System;

namespace SimpleArithmeticExpressionsParser.FunctionHandlers
{
    public class CosFunctionHandler : OneArgumentFunctionHandlerBase
    {
        protected override string FunctionName => nameof(Math.Cos).ToLower();
        
        protected override double ApplyFunction(double value) => Math.Cos(value);
    }
}