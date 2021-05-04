using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.Utility.Models.Interfaces;

namespace Text_Analyzer.Utility.Models
{
    public class Text : IText
    {
        private ICollection<ISentence> _sentences;

        public ICollection<ISentence> Sentences
        {
            get => _sentences;
            set => _sentences = value;
        }

        public Text()
        {
            Sentences = new List<ISentence>();
        }

        public Text(ICollection<ISentence> sentences)
        {
            Sentences = sentences;
        }

        public void Add(ISentence sentence)
        {
            if (Sentences != null)
            {
                Sentences.Add(sentence);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var sentence in Sentences)
            {
                sb.Append(sentence);
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
