namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class NumOperationHandler : OperationHandlerBase
    {
        protected override OperationType OperationType => OperationType.Num;
        
        protected override double ApplyOperation(ITreeNode node) => node.Value;
    }
}