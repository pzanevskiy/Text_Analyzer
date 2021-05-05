using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Text_Analyzer.Models
{
    public class FileLinks
    {
        public int Id { get; set; }
        public UploadedFile UploadedFile { get; set; }
        public FileToDownload FileToDownload { get; set; }
    }
}
