using System;
using System.Diagnostics;
using SimpleArithmeticExpressionsParser;

namespace Test
{
    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var expression = "(ABS(SIN(2521 * 24)) * 2 / COS(214 * (124 - (-52)) / 2 * (6 + 4)) + COS(0)) / 2";
                var parser = new Parser(expression);
                var result = parser.CalculateResult();

                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}