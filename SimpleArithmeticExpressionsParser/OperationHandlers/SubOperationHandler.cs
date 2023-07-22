namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class SubOperationHandler : IOperationHandler
    {
        public OperationType OperationType => OperationType.Sub;
        
        public double Handle(ITreeNode node)
        {
            if (node.OperationType == OperationType)
            {
                return node.Left.Value - node.Right.Value;
            }

            return node.Value;
        }
    }
}