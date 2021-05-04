using DeepMorphy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Text_Analyzer.Utility.Comparer;
using Text_Analyzer.Utility.DataTransferObject;
using Text_Analyzer.Utility.Models;
using Text_Analyzer.Utility.Models.Enums;
using Text_Analyzer.Utility.Models.Interfaces;
using Text_Analyzer.Utility.Service.Interfaces;

namespace Text_Analyzer.Utility.Service
{
    public class TextService : ITextService
    {
        /// <summary>
        /// In all interrogative sentences of the text, find and print without repeating words of a given length
        /// </summary>
        /// <param name="sentences">All text sentences</param>
        /// <param name="length">Word length</param>
        /// <returns>IEnumerable of found words</returns>
        public IEnumerable<IWord> GetInterrogativeSentencesWordsWithLength(ICollection<ISentence> sentences, int length)
        {
            return sentences
                .Where(x => x.TypeOfSentence.Equals(SentenceType.INTERROGATIVE)).ToList()
                .SelectMany(y => y.Words.Where(x => x is IWord word && word.Count == length))
                .Cast<IWord>()
                .Distinct(new WordComparer());

        }

        /// <summary>
        /// In some sentence of the text, replace words of a given length with the specified substring, 
        /// the length of which may not coincide with the length of the word
        /// </summary>
        /// <param name="sentence">All text sentences</param>
        /// <param name="length">Word length</param>
        /// <param name="newWord">Word to replace</param>
        public void ReplaceWords(ISentence sentence, int length, string newWord)
        {
            foreach (var word in sentence.Words.Where(x => x is IWord word && word.Count == length).ToList())
                sentence.Replace(word, new Word(newWord));
        }

        /// <summary>
        /// Display all sentences of a given text in ascending order of the number of words in each of them
        /// </summary>
        /// <param name="sentences">All text sentences</param>
        /// <returns>Sorted sentences</returns>
        public ICollection<ISentence> SortSentences(ICollection<ISentence> sentences)
        {
            return sentences.OrderBy(sentence => sentence.Count).ToList();
        }

        /// <summary>
        /// Remove all words of a given length starting with a consonant from the text
        /// </summary>
        /// <param name="sentences">All text sentences</param>
        /// <param name="length">Word length</param>
        /// <returns>Text without words of given length</returns>
        public ICollection<ISentence> RemoveWordsStartsWithConsonants(ICollection<ISentence> sentences, int length)
        {
            string pattern = @"[\daeiou]";
            var words = sentences
                .SelectMany(x => x.Words
                .Where(y => y is IWord word && word.Count == length && !Regex.IsMatch(word.FirstChar, pattern)));
            foreach (var sentence in sentences)
                foreach (var word in words.ToList())
                    sentence.Remove(word);
            return sentences;
        }

        public IEnumerable<ConcordanceItem> Concordance(IText text)
        {
            return text.Sentences
                .SelectMany(sentence => sentence.Words)
                .GroupBy(word => word.ToString().ToLower())
                .Select(item => new ConcordanceItem { Word = new Word(item.Key), Count = item.ToList().Count })
                .Where(item => item.Word.Count > 0)
                .OrderBy(x => x.Word.FirstChar);
        }

        public IEnumerable<ConcordanceItemsDTO> ConcordanceMorphy(IEnumerable<ConcordanceItem> items)
        {
            var concordances = items.ToDictionary(x => x.Word.ToString());
            var morphAnalyzer = new MorphAnalyzer(withLemmatization: true);
            var results = morphAnalyzer.Parse(concordances.Select(x => x.Key)).ToList();
            var pairs = new Dictionary<string, IList<ConcordanceItem>>();

            foreach (var morphInfo in results)
            {
                var mainWord = morphInfo;
                bool checkpoint = false;
                foreach (var item in pairs)
                {
                    if (item.Value.Select(x => x.Word.ToString()).Contains(mainWord.Text))
                    {
                        checkpoint = true;
                        break;
                    }
                }
                if (checkpoint)
                {
                    continue;
                }
                var strings = new List<ConcordanceItem>();
                foreach (var item in results)
                {
                    if (mainWord.CanBeSameLexeme(item))
                    {
                        strings.Add(concordances[item.Text]);
                    }
                }
                pairs.Add(mainWord.Text, strings);
            }
            return pairs.Select(x => new ConcordanceItemsDTO
            {
                Words = string.Join('/', x.Value.Select(x => x.Word.ToString())),
                Counter = x.Value.Select(x => x.Count).Sum()
            });
        }
    }
}
