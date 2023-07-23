using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleArithmeticExpressionsParser
{
    public class Parser
    {
        private string _expression;
        private Node _root;

        private readonly List<IOperationHandler> _operationHandlers = HandlersFactory<IOperationHandler>.GetHandlers();
        
        private readonly List<IFunctionHandler> _functionHandlers = HandlersFactory<IFunctionHandler>.GetHandlers();

        public string Expression
        {
            get => _expression;
            set => Init(value);
        }

        public Parser()
        {
            _expression = "";
        }

        public Parser(string expression)
        {
            Init(expression);
        }

        public double CalculateResult()
        {
            return TreeTraversal();
        }

        private void Init(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                throw new Exception("Expression must be not empty");
            }
            if (!BracketsHelper.CheckBrackets(expression))
            {
                throw new Exception("Invalid expression format");
            }

            _expression = expression.Replace(" ", "").ToLower();
            _expression = _functionHandlers
                .Aggregate(_expression, (current, functionHandler) => functionHandler.Handle(current));
            _root = BuildTree(_expression);
        }

        

        private Node BuildTree(string expression)
        {
            while (expression[0] == '(' && expression[^1] == ')' 
                                        && expression.Length - 2 > 0
                                        && BracketsHelper.CheckBrackets(expression.Substring(1, expression.Length - 2)))
            {
                expression = expression.Remove(expression.Length - 1, 1).Remove(0, 1);
            }

            var lowestPriorityOperation = OperationPriorityHelper.FindLowestPriorityOperation(expression);
            if (lowestPriorityOperation == -1)
            {
                expression = expression.Replace("(", "").Replace(")", "");
                return new Node()
                {
                    OperationType = OperationType.Num,
                    Value = Double.Parse(expression)
                };
            }

            var result = new Node();
            result.OperationType = expression[lowestPriorityOperation] switch
            {
                '+' => OperationType.Add,
                '-' => OperationType.Sub,
                '/' => OperationType.Div,
                '*' => OperationType.Mul,
                '^' => OperationType.Pow,
                _ => result.OperationType
            };
            result.Right = BuildTree(expression.Substring(lowestPriorityOperation + 1,
                expression.Length - lowestPriorityOperation - 1));
            result.Left = BuildTree(expression.Substring(0, lowestPriorityOperation));

            return result;
        }
        
        private double TreeTraversal()
        {
            if (_root.OperationType == OperationType.Num)
            {
                return _root.Value;
            }
            
            var stack = new Stack<Node>();
            stack.Push(_root);
            
            while (stack.Count != 0)
            {
                var node = stack.Peek();

                if (node.Left.OperationType == OperationType.Num && node.Right.OperationType == OperationType.Num)
                {
                    node.Value += _operationHandlers.Select(x => x.Handle(node)).Sum();
                    node.OperationType = OperationType.Num;
                    stack.Pop();
                }
                else
                {
                    if (node.Right?.OperationType != OperationType.Num)
                    {
                        stack.Push(node.Right as Node);
                    }
                    if (node.Left?.OperationType != OperationType.Num)
                    {
                        stack.Push(node.Left as Node);
                    }
                }
            }

            return _root.Value;
        }
    }
}