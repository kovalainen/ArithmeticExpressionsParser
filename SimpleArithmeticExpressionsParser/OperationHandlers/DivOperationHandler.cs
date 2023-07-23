namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class DivOperationHandler : OperationHandlerBase
    {
        protected override OperationType OperationType => OperationType.Div;
        
        protected override double ApplyOperation(ITreeNode node) => node.Left.Value / node.Right.Value;
    }
}