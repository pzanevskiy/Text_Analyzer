using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Analyzer.Utility.Models.Interfaces
{
    public interface IWord : ISentenceItem
    {
        public int Count { get; }
        public string FirstChar { get; }
    }
}
