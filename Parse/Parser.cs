
using System;
using System.Collections.Generic;

namespace Parse
{
    public class Parser
    {
        public List<HeapElement> Heap{ get; set; }

        public Parser()
        {
            Heap = new List<HeapElement>();
        }

        public string Parse(string input)
        {
            List<string> lines = LexicalAnalysis.Analize(input);

            List<List<Lexem>> linesOfLexems = LexicalAnalysis.Analize(lines);

            foreach (var lineOfLexems in linesOfLexems)
            {
                if (lineOfLexems.Count == 2)
                {
                    if (lineOfLexems[0].Name == "int")
                    {
                        Heap.Add(new HeapElement
                        {
                            Type = AllowedType.Int,
                            Name = lineOfLexems[1].Value as string,
                            Value = 0
                        });
                    }
                    else if (lineOfLexems[0].Name == "string")
                    {
                        Heap.Add(new HeapElement
                        {
                            Type = AllowedType.String,
                            Name = lineOfLexems[1].Value as string,
                            Value = String.Empty
                        });
                    }
                }


            }

            return String.Empty;
        }
    }
}
