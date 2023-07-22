namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class DivOperationHandler : IOperationHandler
    {
        public OperationType OperationType => OperationType.Div;
        
        public double Handle(ITreeNode node)
        {
            if (node.OperationType == OperationType)
            {
                return node.Left.Value / node.Right.Value;
            }

            return node.Value;
        }
    }
}