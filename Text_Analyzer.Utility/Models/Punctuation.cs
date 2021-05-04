using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.Utility.Models.Interfaces;

namespace Text_Analyzer.Utility.Models
{
    public class Punctuation : ISentenceItem
    {
        private Symbol _punctuationMark;

        public Symbol PunctuationMark
        {
            get => _punctuationMark;
            set => _punctuationMark = value;
        }

        public Punctuation(string punctuation)
        {
            this.PunctuationMark = new Symbol(punctuation);
        }

        public override string ToString()
        {
            return PunctuationMark.ToString();
        }
    }
}
