namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class AddOperationHandler : IOperationHandler
    {
        public OperationType OperationType => OperationType.Add;

        public double Handle(ITreeNode node)
        {
            if (node.OperationType == OperationType)
            {
                return node.Right.Value + node.Left.Value;
            }

            return node.Value;
        }
    }
}