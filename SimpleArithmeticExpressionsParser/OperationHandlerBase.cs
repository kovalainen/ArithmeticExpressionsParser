namespace SimpleArithmeticExpressionsParser
{ 
    public abstract class OperationHandlerBase : IOperationHandler
    {
        public double Handle(ITreeNode node)
        {
            if (node.OperationType == OperationType)
            {
                return ApplyOperation(node);
            }

            return 0;
        }
        
        protected abstract OperationType OperationType { get; }

        protected abstract double ApplyOperation(ITreeNode node);
    }
}