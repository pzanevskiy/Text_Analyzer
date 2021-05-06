using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Text_Analyzer.Models
{
    public class FileToDownload
    {
        public int Id { get; set; }
        public string Filename { get; set; }

        public virtual ICollection<FileLinks> FileLinks { get; set; }
    }
}
