using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.BL.DTO;

namespace Text_Analyzer.BL.Service.Interfaces
{
    public interface IUploadedFileService
    {
        IEnumerable<UploadedFileDTO> GetAll();
        UploadedFileDTO FindById(int id);
        void Add(UploadedFileDTO uploadedFileDTO);
        void Delete(int id);
        void Update(UploadedFileDTO uploadedFileDTO);
    }
}
