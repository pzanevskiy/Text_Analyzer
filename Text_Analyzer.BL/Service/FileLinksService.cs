using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.BL.DTO;
using Text_Analyzer.BL.Service.Interfaces;
using Text_Analyzer.BL.Utils;
using Text_Analyzer.DAL.UOW;
using Text_Analyzer.DB;

namespace Text_Analyzer.BL.Service
{
    public class FileLinksService : IFileLinksService
    {
        private IMapper _mapper;

        private IUnitOfWork Database { get; set; }

        public FileLinksService(IUnitOfWork uow)
        {
            Database = uow;
            _mapper = new Mapper(MapperConfigBL.Configure());
        }

        public IEnumerable<FileLinksDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<FileLinks>, IEnumerable<FileLinksDTO>>(Database.FileLinks.Get());
        }

        public void Add(FileLinksDTO fileLinksDTO)
        {
            var uploadedFile = Database.UploadedFiles.Get(x => x.Filename.Equals(fileLinksDTO.UploadedFiles));
            var fileToDownload = Database.FilesToDownload.Get(x => x.Filename.Equals(fileLinksDTO.FilesToDownload));

            var file = _mapper.Map<FileLinksDTO, FileLinks>(fileLinksDTO);

            var fileObj = new FileLinks()
            {
                UploadedFiles = uploadedFile,
                FilesToDownload = fileToDownload
            };
            Console.WriteLine();
        }
    }
}
