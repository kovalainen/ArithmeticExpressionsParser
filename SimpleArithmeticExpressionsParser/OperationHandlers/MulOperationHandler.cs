namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class MulOperationHandler : OperationHandlerBase
    {
        protected override OperationType OperationType => OperationType.Mul;
        
        protected override double ApplyOperation(ITreeNode node) => node.Right.Value * node.Left.Value;
    }
}