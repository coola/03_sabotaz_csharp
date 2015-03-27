using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using NUnit.Framework;
using Parse;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        private Parser Parser { get; set; }
        private LexicalAnalysis LexicalAnalysis { get; set; }

        [SetUp]
        public void Start()
        {
            Parser = new Parser();
            LexicalAnalysis = new LexicalAnalysis();
        }

        [TearDown]
        public void Stop()
        {
            Parser = null;
            LexicalAnalysis = null;
        }

        [TestCase]
        public void InitialTest()
        {
            var parser = new Parser();
            Assert.NotNull(parser);
        }

        [TestCase]
        public void TestEmptyInputEmptyOutput()
        {
            var parser = new Parser();
            
            var input = String.Empty;
            string result = parser.Parse(input);
            Assert.AreEqual(string.Empty, result);
        }

        [TestCase]
        public void TestEmptyOutputWhenThere()
        {
            var input = String.Empty;
            string result = Parser.Parse(input);
            Assert.AreEqual(string.Empty, result);
        }

        [TestCase]
        public void TestLexicalAnalisis()
        {
            var lexicalAnalisis = new LexicalAnalysis();

            Assert.NotNull(lexicalAnalisis);

            var input = String.Empty;

            List<Lexem> lexems = LexicalAnalysis.AnalizeLine(input);

            Assert.NotNull(lexems);

            Assert.AreEqual(0, lexems.Count);
        }

        [TestCase]
        public void TestLexicalAnalisisNoAnalysisData()
        {
            TestCountOfLexems(String.Empty, 0);
        }

        private void TestCountOfLexems(string stringToParse, int expectedNumerOfLexems)
        {

            var lexicalAnalisis = new LexicalAnalysis();

            Assert.NotNull(lexicalAnalisis);

            List<Lexem> lexems = LexicalAnalysis.AnalizeLine(stringToParse);

            Assert.NotNull(lexems);

            Assert.AreEqual(expectedNumerOfLexems, lexems.Count);

        }

        [TestCase]
        public void TestLexicalAnalisisCountSomeAnalysisData()
        {
            TestCountOfLexems("int", 1);
        }

        [TestCase]
        public void TestLexicalAnalisisCountMoreAnalysisData()
        {
            TestCountOfLexems("int a = 5", 4);
        }

        [TestCase]
        public void TestLexicalAnalisisCountMoreAnalysisDataWithSpaces()
        {
            TestCountOfLexems("int   a = 5", 4);
        }

        [TestCase]
        public void TestLexicalAnalisisCountMoreAnalysisDataWithWhiteSpaces()
        {
            TestCountOfLexems("int \n \r\n \t a = 5", 4);
        }

        [TestCase]
        public void TestRecognizationOfIntLexem()
        {
            var analize = LexicalAnalysis.AnalizeLine("int");
            Assert.AreEqual("int", analize[0].Name);
        }

        [TestCase]
        public void TestRecognizationOfStringLexem()
        {
            var analize = LexicalAnalysis.AnalizeLine("string");
            Assert.AreEqual("string", analize[0].Name);
        }

        [TestCase]
        public void TestRecognizationOfStringAndIntLexem()
        {
            var analize = LexicalAnalysis.AnalizeLine("string int");
            Assert.AreEqual("string", analize[0].Name);
            Assert.AreEqual("int", analize[1].Name);
        }

        [TestCase]
        public void TestRecognizationOfVariableLexem()
        {
            var analize = LexicalAnalysis.AnalizeLine("string a");
            Assert.AreEqual("variable", analize[1].Name);

        }

        [TestCase]
        public void TestLexicalSimpleVariableDefinition()
        {
            var analize = LexicalAnalysis.AnalizeLine("a=5");
            Assert.AreEqual(3, analize.Count);
        }

        [TestCase]
        public void TestDivideIntoLinesNoLines()
        {
            List<string> lines = LexicalAnalysis.Analize("");
            Assert.AreEqual(0,lines.Count);
        }

        [TestCase]
        public void TestDivideIntoLinesOneLine()
        {
            List<string> lines = LexicalAnalysis.Analize(";" + Environment.NewLine);
            Assert.AreEqual(1, lines.Count);
        }

        [TestCase]
        [ExpectedException(typeof(CompilationException))]
        public void TestDivideIntoLinesLastLineWithoutSemicolon()
        {
            List<string> lines = LexicalAnalysis.Analize("var a = 5;aa");
            Assert.AreEqual(1, lines.Count);
        }

        [TestCase]
        public void TestAnalysisAltogether()
        {
            var lexicalAnalysis = new LexicalAnalysis();
            List<string> lines = LexicalAnalysis.Analize(String.Format("var a = 5;{0}var b = 4;{0}", Environment.NewLine));

            List<List<Lexem>> linesOfLexems = LexicalAnalysis.Analize(lines);

            Assert.AreEqual(4, linesOfLexems[0].Count);
            Assert.AreEqual(4, linesOfLexems[1].Count);

        }

        

        [TestCase]
        public void TestAnalysisAltogetherMoreComplex()
        {
            var lexicalAnalysis = new LexicalAnalysis();
            List<string> lines = LexicalAnalysis.Analize(String.Format("int a = 5;{0}string b = 4;{0}", Environment.NewLine));

            List<List<Lexem>> linesOfLexems = LexicalAnalysis.Analize(lines);

            Assert.AreEqual("int", linesOfLexems[0][0].Name);
            Assert.AreEqual("string", linesOfLexems[1][0].Name);
            Assert.AreEqual("variable", linesOfLexems[1][1].Name);

        }

        [TestCase]
        public void TestReservedWordPrintAndCast()
        {
            var lexicalAnalysis = new LexicalAnalysis();
            Assert.AreEqual("print", LexicalAnalysis.AnalizeLine("print")[0].Name);
            Assert.AreEqual("cast", LexicalAnalysis.AnalizeLine("cast")[0].Name);
        }

        [TestCase]
        public void TestBracketLexems()
        {
            Assert.AreEqual("(", LexicalAnalysis.AnalizeLine("(")[0].Name);
            Assert.AreEqual(")", LexicalAnalysis.AnalizeLine(")")[0].Name);
        }

        [TestCase]
        public void TestAllOtherLexems()
        {
            TestLexemInLine("\"");
            TestLexemInLine("+");
            TestLexemInLine("-");
            TestLexemInLine("*");
            TestLexemInLine("/");
        }

        private void TestLexemInLine(string lexem)
        {
            Assert.AreEqual(lexem, LexicalAnalysis.AnalizeLine(lexem)[0].Name);
        }

        [TestCase]
        public void TestDifferenceBetweenVariableAndNumber()
        {
            Assert.AreEqual("variable", LexicalAnalysis.AnalizeLine("int assda = 54545")[1].Name);
            Assert.AreEqual("number", LexicalAnalysis.AnalizeLine("int assda = 54545")[3].Name);
        }

        [TestCase]
        public void TestValueOfTheNumber()
        {
            Assert.AreEqual(54545, LexicalAnalysis.AnalizeLine("int assda = 54545")[3].Value);
        }

        [TestCase]
        public void TestTranslationIntoObjects()
        {
           Parser.Parse(String.Format("int d;{0}", Environment.NewLine));
           Assert.AreEqual(1, Parser.Heap.Count);
        }

        [TestCase]
        public void TestNoObjectsOnHeap()
        {
            Parser.Parse(String.Format("d=a;{0}", Environment.NewLine));
            Assert.AreEqual(0, Parser.Heap.Count);
        }

        [TestCase]
        public void TestDeclaringSimpleIntVariable()
        {
            Parser.Parse(String.Format("int d;{0}", Environment.NewLine));
            Assert.AreEqual("d", Parser.Heap[0].Name);
            Assert.AreEqual(AllowedType.Int, Parser.Heap[0].Type);
            Assert.AreEqual(0, Parser.Heap[0].Value);
        }

        [TestCase]
        public void TestDeclaringTwoIntVariables()
        {
            Parser.Parse(String.Format("int d;{0}int g;{0}", Environment.NewLine));
            Assert.AreEqual("g", Parser.Heap[1].Name);
            Assert.AreEqual(AllowedType.Int, Parser.Heap[1].Type);
            Assert.AreEqual(0, Parser.Heap[1].Value);
        }

        [TestCase]
        public void TestDeclaringSimpleStringVariable()
        {
            Parser.Parse(String.Format("string napis;{0}", Environment.NewLine));
            Assert.AreEqual("napis", Parser.Heap[0].Name);
            Assert.AreEqual(AllowedType.String, Parser.Heap[0].Type);
            Assert.AreEqual(String.Empty, Parser.Heap[0].Value);
        }

        [TestCase]
        public void TestDeclaringSimpleStringAndIntVariable()
        {
            Parser.Parse(String.Format("int d;{0}string napis;{0}", Environment.NewLine));

            Assert.AreEqual("d", Parser.Heap[0].Name);
            Assert.AreEqual(AllowedType.Int, Parser.Heap[0].Type);
            Assert.AreEqual(0, Parser.Heap[0].Value);

            CheckVariable(1, "napis", AllowedType.String, String.Empty);
        }

        private void CheckVariable(int index, string name, AllowedType type, object value)
        {
            Assert.AreEqual(name, Parser.Heap[index].Name);
            Assert.AreEqual(type, Parser.Heap[index].Type);
            Assert.AreEqual(value, Parser.Heap[index].Value);
        }

        [TestCase]
        public void TestDefinitionWithNumberOfOnceDeclaredIntVariable()
        {
            Parser.Parse(String.Format("int d;{0}d=5;{0}", Environment.NewLine));
            CheckVariable(0, "d", AllowedType.Int, 5);
       
        }

    }
}
