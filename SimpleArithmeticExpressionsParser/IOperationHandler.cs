namespace SimpleArithmeticExpressionsParser
{
    public interface IOperationHandler
    {
        OperationType OperationType { get; }
        
        double Handle(ITreeNode node);
    }
}