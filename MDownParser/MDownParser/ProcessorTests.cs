using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MDownParser
{
    [TestFixture]
    class processor_should
    {
        [Test]
        public static void find_and_create_paragraphs()
        {
            var input = "lalala\n\nlalala";
            var result = Processor.Process(input);
            Assert.AreEqual("<p>lalala</p><p>lalala</p>",result);

        }

        [Test]
        public static void in_more_complicated_cases()
        {
            var input = "lalala\n     \nlalala";
            var result = Processor.Process(input);
            Assert.AreEqual("<p>lalala</p><p>lalala</p>", result);
        }

        [Test]
        public static void and_when_there_are_lots_of_paragraphs()
        {
            var input = "lalala\n\nlalala\n     \ntratata\n \nblahblah";
            var result = Processor.Process(input);
            Assert.AreEqual("<p>lalala</p><p>lalala</p><p>tratata</p><p>blahblah</p>", result);
        }

        [Test]
        public static void understand_when_its_not_the_time_to_parse()
        {
            var input = "lalala\na\nlalala";
            var result = Processor.Process(input);
            Assert.AreEqual("<p>lalala\na\nlalala</p>", result);
        }


    }
}
