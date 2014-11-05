using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MDownParser
{
    static class Processor
    {
        public static string Process(string markdown)
        {
            return ParseParagraphs(markdown).Select(x => AddTags(x, ParagraphTag))
                                    .Aggregate((x, i) => x + i);
        }

        static string AddTags(string textWithoutTags, string tag)
        {
            var sb = new StringBuilder("<" + tag + ">");
            sb.Append(textWithoutTags);
            sb.Append("</" + tag + ">");
            return sb.ToString();
        }

        private const string ParagraphPattern = @"\n\s*\n";

        private const string ParagraphTag = "p";

        static string[] ParseParagraphs(string text)
        {
            return Regex.Split(text, ParagraphPattern);
        }
    }
}
