using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MDownParser
{
    static class Processor
    {
        private static Dictionary<string, string> tagsDictionary = new Dictionary<string, string>()
        {
            {@"\b_", "<em>"},
            {@"_\b","</em>"}
        };
        public static string Process(string text)
        {
            var paragraphs=Regex.Split(text, @"\n\s*\n");
            return paragraphs.Select(ParagraphProcessor)
                .Select(x=>"<p>"+x+"</p>")
                .Aggregate((x, i) => x + i);
        }

        private static string ParagraphProcessor(string paragraph)
        {
            return ChangeTags(paragraph);
        }

        private static string ChangeTags(string text)
        {
            foreach (var pattern in tagsDictionary.Keys)
            {
                text = Regex.Replace(text, pattern, tagsDictionary[pattern]);
            }
            return text;
        }
    }
    
}
