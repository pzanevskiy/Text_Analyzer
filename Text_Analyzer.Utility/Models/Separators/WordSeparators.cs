using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Analyzer.Utility.Models.Separators
{
    public class WordSeparators : Separator
    {
        string[] wordSeparators = { ",", " - ", ":", ";", "–" };

        public override string[] GetSeparators()
        {
            return wordSeparators;
        }
    }
}
