namespace SimpleArithmeticExpressionsParser
{
    public class Node : ITreeNode
    {
        public double Value { get; set; }
        
        public OperationType OperationType { get; set; }
        
        public ITreeNode Left { get; set; }
        
        public ITreeNode Right { get; set; }
    }
}