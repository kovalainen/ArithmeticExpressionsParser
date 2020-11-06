namespace SimpleArithmeticExpressionsParser
{
    internal class Node
    {
        public double Value { get; set; }
        public Operation Operation { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}