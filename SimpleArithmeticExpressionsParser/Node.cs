namespace SimpleArithmeticExpressionsParser
{
    public class Node : ITreeNode
    {
        public double Value { get; set; }
        
        public OperationType OperationType { get; set; }
        
        public Node Left { get; set; }
        
        public Node Right { get; set; }
    }
}