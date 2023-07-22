namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class MulOperationHandler : IOperationHandler
    {
        public OperationType OperationType => OperationType.Mul;
        
        public double Handle(ITreeNode node)
        {
            if (node.OperationType == OperationType)
            {
                return node.Right.Value * node.Left.Value;
            }

            return node.Value;
        }
    }
}