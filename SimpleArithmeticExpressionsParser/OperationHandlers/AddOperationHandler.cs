namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class AddOperationHandler : OperationHandlerBase
    {
        protected override OperationType OperationType => OperationType.Add;
        
        protected override double ApplyOperation(ITreeNode node) => node.Right.Value + node.Left.Value;
    }
}