namespace SimpleArithmeticExpressionsParser
{
    public interface ITreeNode
    {
        public double Value { get; }
        
        public OperationType OperationType { get;  }
        
        public ITreeNode Left { get; }
        
        public ITreeNode Right { get; }
    }
}