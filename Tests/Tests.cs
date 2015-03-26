using System;
using System.Collections.Generic;
using NUnit.Framework;
using Parse;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
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
            var parser = new Parser();

            var input = String.Empty;
            string result = parser.Parse(input);
            Assert.AreEqual(string.Empty, result);
        }

        [TestCase]
        public void TestLexicalAnalisis()
        {
            var lexicalAnalisis = new LexicalAnalysis();

            Assert.NotNull(lexicalAnalisis);

            var input = String.Empty;

            List<Lexem> lexems = lexicalAnalisis.AnalizeLine(input);

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

            List<Lexem> lexems = lexicalAnalisis.AnalizeLine(stringToParse);

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
            TestCountOfLexems("int a = 5;", 4);
        }

        [TestCase]
        public void TestLexicalAnalisisCountMoreAnalysisDataWithSpaces()
        {
            TestCountOfLexems("int   a = 5;", 4);
        }

        [TestCase]
        public void TestLexicalAnalisisCountMoreAnalysisDataWithWhiteSpaces()
        {
            TestCountOfLexems("int \n \r\n \t a = 5;", 4);
        }

        [TestCase]
        public void TestRecognizationOfIntLexem()
        {
            var analize = new LexicalAnalysis().AnalizeLine("int");
            Assert.AreEqual("int", analize[0].Name);
        }

        [TestCase]
        public void TestRecognizationOfStringLexem()
        {
            var analize = new LexicalAnalysis().AnalizeLine("string");
            Assert.AreEqual("string", analize[0].Name);
        }

        [TestCase]
        public void TestRecognizationOfStringAndIntLexem()
        {
            var analize = new LexicalAnalysis().AnalizeLine("string int");
            Assert.AreEqual("string", analize[0].Name);
            Assert.AreEqual("int", analize[1].Name);
        }

        [TestCase]
        public void TestRecognizationOfVariableLexem()
        {
            var analize = new LexicalAnalysis().AnalizeLine("string a");
            Assert.AreEqual("variable", analize[1].Name);
        }

        [TestCase]
        public void TestDivideIntoLinesNoLines()
        {
            List<string> lines = new LexicalAnalysis().Analize("");
            Assert.AreEqual(0,lines.Count);
        }

        [TestCase]
        public void TestDivideIntoLinesOneLine()
        {
            List<string> lines = new LexicalAnalysis().Analize(";" + Environment.NewLine);
            Assert.AreEqual(1, lines.Count);
        }

        [TestCase]
        [ExpectedException(typeof(CompilationException))]
        public void TestDivideIntoLinesLastLineWithoutSemicolon()
        {
            List<string> lines = new LexicalAnalysis().Analize("var a = 5;aa");
            Assert.AreEqual(1, lines.Count);
        }

        [TestCase]
        public void TestAnalysisAltogether()
        {
            var lexicalAnalysis = new LexicalAnalysis();
            List<string> lines = lexicalAnalysis.Analize(String.Format("var a = 5;{0}var b = 4;{0}", Environment.NewLine));

            List<List<Lexem>> linesOfLexems = lexicalAnalysis.Analize(lines);

            Assert.AreEqual(4, linesOfLexems[0].Count);
            Assert.AreEqual(4, linesOfLexems[1].Count);

        }

        [TestCase]
        public void TestAnalysisAltogetherMoreComplex()
        {
            var lexicalAnalysis = new LexicalAnalysis();
            List<string> lines = lexicalAnalysis.Analize(String.Format("int a = 5;{0}string b = 4;{0}", Environment.NewLine));

            List<List<Lexem>> linesOfLexems = lexicalAnalysis.Analize(lines);

            Assert.AreEqual("int", linesOfLexems[0][0].Name);
            Assert.AreEqual("string", linesOfLexems[1][0].Name);
            Assert.AreEqual("variable", linesOfLexems[1][1].Name);

        }
    }
}
