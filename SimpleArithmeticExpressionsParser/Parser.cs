using System;
using System.Collections.Generic;

namespace SimpleArithmeticExpressionsParser
{
    public class Parser
    {
        private string _expression;
        private Node _root;

        private readonly List<Tuple<string, Func<double, double>>> _functionsWithOneArgument =
            new List<Tuple<string, Func<double, double>>>()
            {
                new Tuple<string, Func<double, double>>("cos", Math.Cos),
                new Tuple<string, Func<double, double>>("sin", Math.Sin),
                new Tuple<string, Func<double, double>>("abs", Math.Abs),
                new Tuple<string, Func<double, double>>("sqr", Math.Sqrt),
            };

        private readonly Dictionary<int, Predicate<char>> _operations = new Dictionary<int, Predicate<char>>()
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
                throw new Exception("Expression must be not empty");
            if (!CheckBrackets(expression))
                throw new Exception("Invalid expression format");

            _expression = expression.Replace(" ", "").ToLower();
            _expression = HandleFunctions(_expression);
            _root = BuildTree(_expression);
        }

        private bool CheckBrackets(string expression)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char ch in expression)
            {
                switch (ch)
                {
                    case '(':
                        stack.Push(ch);
                        break;
                    case ')' when stack.Count != 0:
                        stack.Pop();
                        break;
                    case ')':
                        return false;
                }
            }

            return stack.Count == 0;
        }

        private Node BuildTree(string expression)
        {
            while (expression[0] == '(' && expression[^1] == ')')
            {
                expression = expression.Remove(expression.Length - 1, 1).Remove(0, 1);
            }

            int lowestPriorityOperation = FindLowestPriorityOperation(expression);
            if (lowestPriorityOperation == -1)
            {
                expression = expression.Replace("(", "").Replace(")", "");
                return new Node()
                {
                    Operation = Operation.Num,
                    Value = Double.Parse(expression)
                };
            }

            Node result = new Node();
            result.Operation = expression[lowestPriorityOperation] switch
            {
                '+' => Operation.Add,
                '-' => Operation.Sub,
                '/' => Operation.Div,
                '*' => Operation.Mul,
                '^' => Operation.Pow,
                _ => result.Operation
            };
            result.Right = BuildTree(expression.Substring(lowestPriorityOperation + 1,
                expression.Length - lowestPriorityOperation - 1));
            result.Left = BuildTree(expression.Substring(0, lowestPriorityOperation));

            return result;
        }

        private int FindLowestPriorityOperation(string expression)
        {
            int result = -1;

            for (int j = 0; j < _operations.Count; j++)
            {
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(') i = SkipBrackets(i, expression);
                    if (i < expression.Length && i != 0 && (char.IsDigit(expression[i - 1]) || expression[i - 1] == ')')
                        && _operations[j](expression[i]))
                        result = i;
                }

                if (result != -1) return result;
            }

            return result;
        }

        private double TreeTraversal()
        {
            if (_root.Operation == Operation.Num)
                return _root.Value;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(_root);
            while (stack.Count != 0)
            {
                Node node = stack.Peek();

                if (node.Left.Operation == Operation.Num && node.Right.Operation == Operation.Num)
                {
                    node.Value += node.Operation switch
                    {
                        Operation.Add => node.Left.Value + node.Right.Value,
                        Operation.Sub => node.Left.Value - node.Right.Value,
                        Operation.Div => node.Left.Value / node.Right.Value,
                        Operation.Mul => node.Left.Value * node.Right.Value,
                        Operation.Pow => Math.Pow(node.Left.Value, node.Right.Value),
                        Operation.Num => node.Value,
                        _ => 0.0
                    };
                    node.Operation = Operation.Num;
                    stack.Pop();
                }
                else
                {
                    if (node.Right?.Operation != Operation.Num) stack.Push(node.Right);
                    if (node.Left?.Operation != Operation.Num) stack.Push(node.Left);
                }
            }

            return _root.Value;
        }

        private int SkipBrackets(int from, string expression)
        {
            int open = 0;
            int close = 0;
            for (int i = from; i < expression.Length; i++)
            {
                if (expression[i] == '(') open++;
                if (expression[i] == ')') close++;
                if (open == close) return i + 1;
            }

            return expression.Length;
        }

        private string HandleFunctions(string expression)
        {
            Parser parser = new Parser();

            foreach (Tuple<string, Func<double, double>> function in _functionsWithOneArgument)
            {
                while (expression.Contains(function.Item1))
                {
                    int indexOfCos = expression.IndexOf(function.Item1, StringComparison.Ordinal);
                    int to = SkipBrackets(indexOfCos + function.Item1.Length, expression);
                    string withoutFunc = expression
                        .Substring(indexOfCos + function.Item1.Length, to - indexOfCos - function.Item1.Length);
                    parser.Expression = withoutFunc;
                    double result = function.Item2(parser.CalculateResult());
                    expression = expression.Replace(function.Item1 + withoutFunc, result.ToString());
                }
            }

            return expression;
        }
    }
}