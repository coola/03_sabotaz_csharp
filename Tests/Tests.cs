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

            List<Lexem> lexems = lexicalAnalisis.Analize(input);

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

            List<Lexem> lexems = lexicalAnalisis.Analize(stringToParse);

            Assert.NotNull(lexems);

            Assert.AreEqual(expectedNumerOfLexems, lexems.Count);

        }

        [TestCase]
        public void TestLexicalAnalisisSomeAnalysisData()
        {
            TestCountOfLexems("int", 1);
        }

    
    }
}
