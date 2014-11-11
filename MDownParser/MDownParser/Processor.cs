using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
            return paragraphs.Select(x => "<p>" + ParagraphPreProcessor(x) + "</p>")
                .Aggregate((x,i)=>x+i);
        }

        static string ParagraphProcessor(string preProcessed)
        {
            var result = new StringBuilder();
            for (int i = 0; i < preProcessed.Length; i++)
            {
                if (preProcessed[i] == '#')
                {
                    switch (preProcessed[i+1])
                    {
                        case ('1'):
                            result.Append("<em>");
                            break;
                        case ('2'):
                            result.Append("</em>");
                            break;
                        case ('3'):
                            result.Append("<strong>");
                            break;
                        case ('4'):
                            result.Append("</strong>");
                            break;
                        case('#'):
                            result.Append("#");
                            break;
                    }
                }
                else
                {
                    result.Append(preProcessed[i]);
                }
            }
            return result.ToString();
        }
        //Превратит верные законопослушные markdown-теги в #'%d' теги, так же обычную # заменит на ##, ибо экранирование.
        //Таблица для превращения:
        //1.'_'-открытие
        //2.'_'-закрытие
        //3.'__'-открытие
        //4.'__'-закрытие
        static string ParagraphPreProcessor(string paragraph)
        {
            var openedTags = new Stack<Tuple<int, string>>();
            var lastTag = "";
            var result = new StringBuilder();
            for (int i = 0; i < paragraph.Length; i++)
            {
                if (paragraph[i] == '_')
                {
                    if ((i < paragraph.Length - 2 && paragraph[i + 1] == '_') && IsValidTag(paragraph, i, "__", true))
                    {
                        result.Append("#3");
                        lastTag = "__";
                        openedTags.Push(Tuple.Create(i, "__"));
                        continue;
                    }
                    if (lastTag == "__")
                    {
                        if (IsValidTag(paragraph, i, "__", false)) ;
                        {
                            result.Append("#4");
                            openedTags.Pop();
                            lastTag = openedTags.Count > 0 ? openedTags.Peek().Item2 : "";
                        }
                    }
                    if (IsValidTag(paragraph, i, "_", true))
                    {
                        result.Append("#1");
                        lastTag = "_";
                        openedTags.Push(Tuple.Create(i, "_"));
                        continue;
                    }
                    if (lastTag == "_")
                    {
                        if (IsValidTag(paragraph, i, "_", false))
                        {
                            result.Append("#2");
                            openedTags.Pop();
                            lastTag = openedTags.Count > 0 ? openedTags.Peek().Item2 : "";
                            continue;
                        }
                    }
                }
                else
                {
                    if (paragraph[i] == '#')
                    {
                        result.Append("##");
                        continue;
                    }
                    result.Append(paragraph[i]);
                }
            }
            while (openedTags.Count!=0)
            {
                var tag = openedTags.Pop();
                if (tag.Item2 == "_")
                {
                    result.Replace("#1", "_", tag.Item1, 1);
                }
                if (tag.Item2 == "__")
                {
                    result.Replace("#3", "__", tag.Item1, 1);
                }
            }
            return result.ToString();
        }


        //Проверяет является ли тэг находящийся в определенном месте условиям открывающегося/закрывающегося тэга
        static bool IsValidTag(string text, int position, string tag,bool IsOpening)
        {
            switch (IsOpening)
            {
                case (true):
                    return (position == 0 || text[position - 1] == ' ');
                    break;
                    
                case (false):
                    return (position == text.Length - tag.Length || text[position + tag.Length] == ' ');
                    break;  
            }
            throw new ArgumentException("bool parameter is not true. and it's not false. magic.");
        }
    }



    static class MyExtensions
    {
        public static void RemoveLast<T>(this List<T> list)
        {
            if (list.Count > 0)
            {
                list.RemoveAt(list.Count);
            }
        }
    }
}
