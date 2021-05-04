using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Text_Analyzer.Utility.Models.Enums;
using Text_Analyzer.Utility.Models.Interfaces;

namespace Text_Analyzer.Utility.Models
{
    public class Sentence : ISentence
    {
        private IList<ISentenceItem> _sentenceItems;
        private SentenceType _sentenceType;

        public IList<ISentenceItem> SentenceItems
        {
            get => _sentenceItems;
            set => _sentenceItems = value;
        }

        public SentenceType TypeOfSentence
        {
            get => _sentenceType;
            set => _sentenceType = value.Equals(null) ? SentenceType.NARRATIVE : value;
        }

        public int Count => _sentenceItems.Where(x => x is IWord word).Count();

        public IList<ISentenceItem> Words => _sentenceItems.Where(x => x is IWord).ToList();

        public Sentence()
        {
            SentenceItems = new List<ISentenceItem>();
            TypeOfSentence = SentenceType.NARRATIVE;
        }

        public Sentence(IList<ISentenceItem> words)
        {
            SentenceItems = words;
        }

        public void Add(ISentenceItem item)
        {
            SentenceItems.Add(item);
        }

        public void Remove(ISentenceItem item)
        {
            SentenceItems.Remove(item);
        }

        public void Replace(ISentenceItem oldItem, ISentenceItem newItem)
        {
            SentenceItems[SentenceItems.IndexOf(oldItem)] = newItem;
        }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < SentenceItems.Count; i++)
            {
                if (SentenceItems[i] is IWord && SentenceItems[i + 1] is Punctuation)
                {
                    stringBuilder.Append(SentenceItems[i]);
                    continue;
                }
                stringBuilder.Append(SentenceItems[i]);
                stringBuilder.Append(" ");
            }
            return stringBuilder.ToString();
        }
    }
}
