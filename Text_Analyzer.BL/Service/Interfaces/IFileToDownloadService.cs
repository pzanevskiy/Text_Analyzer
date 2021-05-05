using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.BL.DTO;

namespace Text_Analyzer.BL.Service.Interfaces
{
    public interface IFileToDownloadService
    {
        IEnumerable<FileToDownloadDTO> GetAll();
        FileToDownloadDTO FindById(int id);
        void Add(FileToDownloadDTO fileToDownloadDTO);
        void Delete(int id);
        void Update(FileToDownloadDTO fileToDownloadDTO);
    }
}
