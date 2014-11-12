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

        [Test]
        public static void put_grounded_parts_in_em_tag()
        {
            var input = "_lalala_";
            var result = Processor.Process(input);
            Assert.AreEqual("<p><em>lalala</em></p>", result);
        }

        [Test]
        public static void put_double_grounded_parts_in_strong_tag()
        {
            var input = "__lalala__";
            var result = Processor.Process(input);
            Assert.AreEqual("<p><strong>lalala</strong></p>", result);
        }

        [Test]
        public static void work_with_one_inside_another()
        {
            var input = "yyy _xxx __lalala__ xxx_ yyy";
            var result = Processor.Process(input);
            Assert.AreEqual("<p>yyy <em>xxx <strong>lalala</strong> xxx</em> yyy</p>", result);
        }

        [Test]
        public static void emphasis_inside_strong()
        {
            var input = "yyy __xxx _lalala_ xxx __ yyy";
            var result = Processor.Process(input);
            Assert.AreEqual("<p>yyy <strong>xxx <em>lalala</em> xxx </strong> yyy</p>", result);
        }

    }
}
