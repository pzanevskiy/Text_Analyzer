using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Analyzer.Utility.Models
{
    public class Symbol
    {
        private string _chars;

        public string Chars
        {
            get => _chars;
            set => _chars = value;
        }

        public Symbol(char ch)
        {
            Chars = String.Format("{0}", ch);
        }

        public Symbol(string chars)
        {
            Chars = chars;
        }

        public override string ToString()
        {
            return _chars;
        }
    }
}
