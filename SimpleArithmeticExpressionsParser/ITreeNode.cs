namespace SimpleArithmeticExpressionsParser
{
    public interface ITreeNode
    {
        public double Value { get; }
        
        public OperationType OperationType { get;  }
        
        public Node Left { get; }
        
        public Node Right { get; }
    }
}