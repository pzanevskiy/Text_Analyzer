using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.BL.DTO;

namespace Text_Analyzer.BL.Service.Interfaces
{
    public interface IFileLinksService
    {
        IEnumerable<FileLinksDTO> GetAll();
        void Add(FileLinksDTO fileLinksDTO);
    }
}
