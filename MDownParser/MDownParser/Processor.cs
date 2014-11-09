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
        public static string Process(string text)
        {
            var paragraphs=Regex.Split(text, @"\n\s*\n");
            return paragraphs.Select(x => "<p>" + x + "</p>")
                .Aggregate((x, i) => x + i);
        }

        private static void GetBorders(string paragraph)
        {

        }

        private static List<Tuple<int, int>> blockBorders;

        
    }
}
