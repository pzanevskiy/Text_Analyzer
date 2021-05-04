using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Text_Analyzer.Utility.Models;
using Text_Analyzer.Utility.Models.Enums;
using Text_Analyzer.Utility.Models.Interfaces;
using Text_Analyzer.Utility.Models.Separators;
using Text_Analyzer.Utility.Service.Interfaces;

namespace Text_Analyzer.Utility.Service
{
    public class Parser : IParser
    {
        private WordSeparators _wordSeparators;
        private SentenceSeparators _sentenceSeparators;
        private OpeningSeparators _openingSeparators;
        private ClosingSeparators _closingSeparators;

        public Parser()
        {
            _wordSeparators = new WordSeparators();
            _sentenceSeparators = new SentenceSeparators();
            _openingSeparators = new OpeningSeparators();
            _closingSeparators = new ClosingSeparators();
        }

        public IText ParseText(ICollection<string> strings)
        {
            IText text = new Text();
            StringBuilder sb = new StringBuilder();

            foreach (var item in strings)
            {
                StringBuilder line = new StringBuilder(item);
                if (sb.ToString() != "")
                {
                    line.Insert(0, sb);
                    sb.Clear();
                }
                var sentences = line.ToString().Split(_sentenceSeparators.GetSeparators(), StringSplitOptions.RemoveEmptyEntries);
                var separators = line.ToString().Split(sentences, StringSplitOptions.RemoveEmptyEntries);
                if (sentences.Length > separators.Length)
                {
                    sb.Append(sentences[sentences.Length - 1]);
                    sb.Append(" ");
                }
                IEnumerable<Tuple<string, string>> tuples = sentences.Zip(separators, (sentence, separator) => new Tuple<string, string>(sentence, separator));
                foreach (var tuple in tuples)
                {
                    text.Add(ParseSentence(tuple));
                }
            }

            return text;
        }
        public IText ParseText(StreamReader reader)
        {
            IText text = new Text();
            StringBuilder sb = new StringBuilder();
            using (reader)
            {

                while (reader.Peek() >= 0)
                {
                    StringBuilder line = new StringBuilder(reader.ReadLine());
                    if (sb.ToString() != "")
                    {
                        line.Insert(0, sb);
                        sb.Clear();
                    }
                    var sentences = line.ToString().Split(_sentenceSeparators.GetSeparators(), StringSplitOptions.RemoveEmptyEntries);
                    var separators = line.ToString().Split(sentences, StringSplitOptions.RemoveEmptyEntries);
                    if (sentences.Length > separators.Length)
                    {
                        sb.Append(sentences[sentences.Length - 1]);
                        sb.Append(" ");
                    }
                    IEnumerable<Tuple<string, string>> tuples = sentences.Zip(separators, (sentence, separator) => new Tuple<string, string>(sentence, separator));
                    foreach (var tuple in tuples)
                    {
                        text.Add(ParseSentence(tuple));
                    }
                }
            }
            return text;
        }

        private ISentence ParseSentence(Tuple<string, string> sentenceTuple)
        {
            ISentence sentence = new Sentence();
            var words = sentenceTuple.Item1.Split(new string[] { " ", "\t", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                var temporaryWord = word;
                var separator = _wordSeparators.GetSeparators().Where(x => word.IndexOf(x) >= 0).FirstOrDefault();
                var openSeparator = _openingSeparators.GetSeparators().Where(x => word.IndexOf(x) >= 0).FirstOrDefault();
                var closeSeparator = _closingSeparators.GetSeparators().Where(x => word.IndexOf(x) >= 0).FirstOrDefault();

                if (openSeparator != null)
                {
                    temporaryWord = temporaryWord.Replace(openSeparator, "");
                    sentence.Add(new Punctuation(openSeparator));
                }

                if (separator == word.ToString())
                {
                    sentence.Add(new Punctuation(word));
                    continue;
                }
                else
                {
                    sentence.Add(new Word(word.Replace(openSeparator ?? " ", "").Replace(separator ?? " ", "").Replace(closeSeparator ?? " ", "")));
                }

                if (separator != null)
                {
                    temporaryWord = temporaryWord.Replace(separator, "");
                    sentence.Add(new Punctuation(separator));
                }
                if (closeSeparator != null)
                {
                    temporaryWord = temporaryWord.Replace(closeSeparator, "");
                    sentence.Add(new Punctuation(closeSeparator));
                }
            }

            SetSentenceType(sentence, sentenceTuple.Item2);
            sentence.Add(new Punctuation(sentenceTuple.Item2));
            return sentence;
        }

        private void SetSentenceType(ISentence sentence, string endMark)
        {
            switch (endMark)
            {
                case ".":
                    {
                        sentence.TypeOfSentence = SentenceType.NARRATIVE;
                        break;
                    }
                case "?":
                    {
                        sentence.TypeOfSentence = SentenceType.INTERROGATIVE;
                        break;
                    }
                case "!":
                    {
                        sentence.TypeOfSentence = SentenceType.EXCLAMATION;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
