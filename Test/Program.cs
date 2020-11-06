using System;
using SimpleArithmeticExpressionsParser;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string expression = "(ABS(SIN(2521 * 24)) * 2 / COS(214 * (124 - (-52)) / 2 * (6 + 4)) + COS(0)) / 2";
                Parser parser = new Parser(expression);
                double result = parser.CalculateResult();
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