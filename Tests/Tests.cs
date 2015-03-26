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
        public void TestLexicalAnalisisSomeAnalysisData()
        {
            var lexicalAnalisis = new LexicalAnalysis();

            Assert.NotNull(lexicalAnalisis);

            var input = String.Empty;

            List<Lexem> lexems = lexicalAnalisis.Analize(input);

            Assert.NotNull(lexems);

            Assert.AreEqual(0, lexems.Count);
        }

    
    }
}
