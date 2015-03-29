using System;
using System.Linq;
using Parse;

namespace parser
{
    class Program
    {
        private static void Main(string[] args)
        {

            // Niestety w tym tygodniu udało mi się zamiplementować tylko prostą analizę leksykalną
            // pozdrawiam serdecznie
            // Michał Kuliński

            if (args.Any())
            {
                var inputString = args[0];
                try
                {
                    new Parser().Parse(inputString);
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.Message);
                    Console.Out.WriteLine(e.StackTrace);
                }
            }
            else
            {
                Console.Out.WriteLine("Please provide file name");
            }
        }
    }
}
