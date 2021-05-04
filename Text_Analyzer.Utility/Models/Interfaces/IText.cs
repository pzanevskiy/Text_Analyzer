using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Analyzer.Utility.Models.Interfaces
{
    public interface IText
    {
        public ICollection<ISentence> Sentences { get; set; }

        public void Add(ISentence sentence);
    }
}
