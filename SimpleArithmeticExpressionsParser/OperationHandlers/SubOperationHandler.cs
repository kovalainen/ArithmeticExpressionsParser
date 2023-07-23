namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class SubOperationHandler : OperationHandlerBase
    {
        protected override OperationType OperationType => OperationType.Sub;
        
        protected override double ApplyOperation(ITreeNode node) => node.Left.Value - node.Right.Value;
    }
}