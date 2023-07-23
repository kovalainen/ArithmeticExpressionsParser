namespace SimpleArithmeticExpressionsParser
{
    public interface IOperationHandler
    {
        double Handle(ITreeNode node);
    }
}