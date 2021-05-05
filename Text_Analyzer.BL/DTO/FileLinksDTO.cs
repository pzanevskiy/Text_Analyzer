using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Analyzer.BL.DTO
{
    public class FileLinksDTO
    {
        public int Id { get; set; }
        public string UploadedFiles { get; set; }
        public string FilesToDownload { get; set; }
    }
}
