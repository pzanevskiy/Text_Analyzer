using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Text_Analyzer.Utility.Models.Interfaces;

namespace Text_Analyzer.Utility.Models
{
    public class Word : IWord
    {
        private ICollection<Symbol> _symbols;

        public ICollection<Symbol> Symbols
        {
            get => _symbols;
            set => _symbols = value;
        }

        public int Count => _symbols.Count;

        public string FirstChar => _symbols.FirstOrDefault().Chars;

        public Word(string word)
        {

            if (word != null)
            {
                this.Symbols = word.Select(x => new Symbol(x)).ToList();
            }
            else
            {
                this.Symbols = null;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var symbol in Symbols)
            {
                sb.Append(symbol);
            }
            return sb.ToString();
        }


    }
}
