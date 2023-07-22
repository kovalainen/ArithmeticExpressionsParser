using System;

namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class PowOperationHandler : IOperationHandler
    {
        public OperationType OperationType => OperationType.Pow;
        
        public double Handle(ITreeNode node)
        {
            if (node.OperationType == OperationType)
            {
                return Math.Pow(node.Left.Value, node.Right.Value);
            }

            return node.Value;
        }
    }
}