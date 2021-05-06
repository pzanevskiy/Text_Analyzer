using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Text_Analyzer.Models
{
    public class FileLinks
    {
        public int Id { get; set; }

        public int UploadedFileId { get; set; }
        public virtual UploadedFile UploadedFile { get; set; }

        public int FileToDownloadId { get; set; }
        public virtual FileToDownload FileToDownload { get; set; }
    }
}
