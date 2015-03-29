using System;
using System.Collections.Generic;

namespace Parse
{
    public class Parser
    {
        public List<HeapElement> Heap { get; set; }

        public Parser()
        {
            Heap = new List<HeapElement>();
        }

        public string Parse(string input)
        {
            List<string> lines = LexicalAnalysis.Analize(input);

            List<List<Lexem>> linesOfLexems = LexicalAnalysis.Analize(lines);

            for (int index = 0; index < linesOfLexems.Count; index++)
            {
                var lineOfLexems = linesOfLexems[index];

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

                if (lineOfLexems.Count == 3)
                {
                    if (lineOfLexems[0].Name == "variable" && lineOfLexems[1].Name == "=" &&
                        lineOfLexems[2].Name == "number")
                    {
                        InsertVariableIntoHeap(lineOfLexems[0], lineOfLexems[2], index);
                    }
                }

                if (lineOfLexems.Count == 5)
                {
                    if (lineOfLexems[0].Name == "variable" && lineOfLexems[1].Name == "=" &&
                        lineOfLexems[3].Name == "stringValue")
                    {
                        InsertVariableIntoHeap(lineOfLexems[0], lineOfLexems[3], index);
                    }
                }
            }

            return String.Empty;
        }

        private void InsertVariableIntoHeap(Lexem varibleLexem, Lexem valueLexem, int lineNumber)
        {
            Predicate<HeapElement> currentElement =
                element => element.Name == varibleLexem.Value.ToString();

            if (Heap.Exists(currentElement))
            {
                var heapElement = Heap.Find(currentElement);
                var removed = Heap.Remove(heapElement);
                if (removed)
                {
                    heapElement.Value = valueLexem.Value;
                    Heap.Insert(Heap.Count, heapElement);
                }
            }
            else
            {
                throw new CompilationException(
                    String.Format("Line:{0} : There is no '{1}' variable declared.", lineNumber,
                        varibleLexem.Name));
            }
        }
    }
}