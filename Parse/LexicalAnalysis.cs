using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parse
{
    public class LexicalAnalysis
    {
        public List<Lexem> Analize(string input)
        {
            var lexems = new List<Lexem>();

            if (!String.IsNullOrEmpty(input))
            {
                string[] splittedByWhitespaces = input.Split(null);

                foreach (var lexem in splittedByWhitespaces)
                {
                   lexems.Add(new Lexem());
                }

               
            }

            return lexems;

        }
    }
}
