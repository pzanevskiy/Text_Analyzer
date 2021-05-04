using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.Utility.DataTransferObject;
using Text_Analyzer.Utility.Models.Interfaces;

namespace Text_Analyzer.Utility.Service.Interfaces
{
    public interface IFileService
    {
        public ICollection<string> GetData(string path, string contentType);
        public void Write(IText text, string filename);
        public void WriteData(IEnumerable<ConcordanceItemsDTO> items, string filename);
    }
}
