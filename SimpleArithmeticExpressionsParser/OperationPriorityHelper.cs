using System;
using System.Collections.Generic;

namespace SimpleArithmeticExpressionsParser
{
    public static class OperationPriorityHelper
    {
        private static readonly Dictionary<int, Predicate<char>> OperationPriorityDictionary = 
            new Dictionary<int, Predicate<char>>
            {
                {0, c => c == '-' || c == '+'},
                {1, c => c == '/' || c == '*' || c == '%'},
                {2, c => c == '^'},
            };
        
        public static int FindLowestPriorityOperation(string expression)
        {
            var result = -1;

            for (var j = 0; j < OperationPriorityDictionary.Count; j++)
            {
                for (var i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(')
                    {
                        i = BracketsHelper.SkipBrackets(i, expression);
                    }
                    if (i < expression.Length && i != 0 && (char.IsDigit(expression[i - 1]) || expression[i - 1] == ')')
                        && OperationPriorityDictionary[j](expression[i]))
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
    }
}