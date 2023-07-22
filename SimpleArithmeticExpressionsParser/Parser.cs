using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SimpleArithmeticExpressionsParser.OperationHandlers;

namespace SimpleArithmeticExpressionsParser
{
    public class Parser
    {
        private string _expression;
        private Node _root;

        private readonly List<IOperationHandler> _operationHandlers = OperationHandlersFactory.CreateHandlers();

        private readonly List<Tuple<string, Func<double, double>>> _functionsWithOneArgument =
            new List<Tuple<string, Func<double, double>>>
            {
                new Tuple<string, Func<double, double>>(nameof(Math.Cos).ToLower(), Math.Cos),
                new Tuple<string, Func<double, double>>(nameof(Math.Sin).ToLower(), Math.Sin),
                new Tuple<string, Func<double, double>>(nameof(Math.Abs).ToLower(), Math.Abs),
                new Tuple<string, Func<double, double>>(nameof(Math.Sqrt).ToLower(), Math.Sqrt),
            };

        private readonly Dictionary<int, Predicate<char>> _operationPriorityDictionary = 
            new Dictionary<int, Predicate<char>>
            {
                {0, c => c == '-' || c == '+'},
                {1, c => c == '/' || c == '*' || c == '%'},
                {2, c => c == '^'},
            };

        public string Expression
        {
            get => _expression;
            set => Init(value);
        }

        private Parser()
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
            _expression = HandleFunctions(_expression);
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

            var lowestPriorityOperation = FindLowestPriorityOperation(expression);
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

        private int FindLowestPriorityOperation(string expression)
        {
            var result = -1;

            for (var j = 0; j < _operationPriorityDictionary.Count; j++)
            {
                for (var i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        i = BracketsHelper.SkipBrackets(i, expression);
                    }
                    if (i < expression.Length && i != 0 && (char.IsDigit(expression[i - 1]) || expression[i - 1] == ')')
                        && _operationPriorityDictionary[j](expression[i]))
                    {
                        result = i;
                    }
                }

                if (result != -1)
                {
                    return result;
                }
            }

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
                        stack.Push(node.Right);
                    }
                    if (node.Left?.OperationType != OperationType.Num)
                    {
                        stack.Push(node.Left);
                    }
                }
            }

            return _root.Value;
        }

        

        private string HandleFunctions(string expression)
        {
            var parser = new Parser();

            foreach (var function in _functionsWithOneArgument)
            {
                while (expression.Contains(function.Item1))
                {
                    var index = expression.IndexOf(function.Item1, StringComparison.Ordinal);
                    var to = BracketsHelper.SkipBrackets(index + function.Item1.Length, expression);
                    var withoutFunc = expression
                        .Substring(index + function.Item1.Length, to - index - function.Item1.Length);
                    parser.Expression = withoutFunc;
                    var result = function.Item2(parser.CalculateResult());
                    expression = expression.Replace(function.Item1 + withoutFunc, result.ToString());
                }
            }

            return expression;
        }
    }
}