using System;

namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class PowOperationHandler : OperationHandlerBase
    {
        protected override OperationType OperationType => OperationType.Pow;
        
        protected override double ApplyOperation(ITreeNode node) => Math.Pow(node.Left.Value, node.Right.Value);
    }
}