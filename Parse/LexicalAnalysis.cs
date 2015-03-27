using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace Parse
{
    public class LexicalAnalysis
    {
        public static List<Lexem> AnalizeLine(string input)
        {
            var lexems = new List<Lexem>();

            if (!String.IsNullOrEmpty(input))
            {
                string[] splittedByWhitespaces = input.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                foreach (var lexem in splittedByWhitespaces)
                {
                    Lexem item;

                    if (lexem == "int" || lexem == "string" || lexem == "=" || lexem == "cast" || lexem == "print"
                        || lexem == "(" || lexem == ")" || lexem == "\"" || lexem == "+" || lexem == "-"
                        || lexem == "/" || lexem == "*")
                    {
                        item = new Lexem { Name = lexem };
                    }
                    else
                    {
                        if (Regex.IsMatch(lexem, "[a-zA-Z_][a-zA-Z0-9_]*", RegexOptions.CultureInvariant))
                        {
                            item = new Lexem {Name = "variable", Value = lexem};
                        }
                        else
                        {
                            item = new Lexem { Name = "number", Value = Int32.Parse(lexem)};
                        }
                       
                    }

                    lexems.Add(item);
                }
            }

            return lexems;

        }

        public static List<string> Analize(string input)
        {
            List<string> analize;

            if (!String.IsNullOrEmpty(input))
            {
                var strings = Regex.Split(input, ";" + Environment.NewLine, RegexOptions.CultureInvariant).ToList();

                var last = strings.Last();

                if(last.Length > 0) throw new CompilationException("Commands must end with semicolon an end line sign. Last command is without semicolon and end line sign.");

                strings.Remove(last);
                analize = strings;
            }
            else
            {
                analize = new List<string>();
            }

            return analize;
           
        }

        public static List<List<Lexem>> Analize(List<string> lines)
        {

            var result = new List<List<Lexem>>();

            foreach (var line in lines)
            {
                result.Add(AnalizeLine(line));
            }

            return result;
        }
    }

    public class CompilationException : Exception
    {
        public CompilationException(string message) : base(message)
        {
          
        }
    }
}
