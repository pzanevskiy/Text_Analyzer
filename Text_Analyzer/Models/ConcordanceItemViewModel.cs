using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextParser.Models.Interfaces;

namespace Text_Analyzer.Models
{
    public class ConcordanceItemViewModel
    {
        public IWord Word { get; set; }
        public int Count { get; set; }
    }
}
