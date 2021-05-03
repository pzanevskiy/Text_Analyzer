using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Text_Analyzer.Models
{
    public class FileViewModel
    {
        public string FileInfo { get; set; }
        public IEnumerable<ConcordanceItemViewModel> Items { get; set; }
    }
}
