namespace SimpleArithmeticExpressionsParser.OperationHandlers
{
    public class NumOperationHandler : IOperationHandler
    {
        public OperationType OperationType => OperationType.Num;
        
        public double Handle(ITreeNode node)
        {
            return node.Value;
        }
    }
}