using System.Collections.Generic;

namespace SimpleArithmeticExpressionsParser
{
    public static class BracketsHelper
    {
        public static int SkipBrackets(int from, string expression)
        {
            var open = 0;
            var close = 0;
            for (var i = from; i < expression.Length; i++)
            {
                switch (expression[i])
                {
                    case '(':
                        open++;
                        break;
                    case ')':
                        close++;
                        break;
                }

                if (open == close)
                {
                    return i + 1;
                }
            }

            return expression.Length;
        }

        public static bool CheckBrackets(string expression)
        {
            var stack = new Stack<char>();
            foreach (var ch in expression)
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
    }
}