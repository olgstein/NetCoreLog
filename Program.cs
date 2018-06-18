using System;
using System.Collections.Generic;
using NetCoreLog.Expressions;

namespace NetCoreLog
{
    class Program
    {
        static void Main(string[] args)
        {
            var expressions = new List<Expression>();
            var parser = new Parser();

            Console.WriteLine("Fact or rule : ");
            for (var code = Console.ReadLine(); code != "end"; code = Console.ReadLine())
            {
                try
                {
                    var expression = parser.Parse(code);
                    expressions.Add(expression);
                    Console.WriteLine(expression.ToString());
                }
                catch(NetCoreLogException ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }
            }
        }
    }
}
