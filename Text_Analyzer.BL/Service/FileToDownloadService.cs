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
    public class FileToDownloadService : IFileToDownloadService
    {
        private IMapper _mapper;

        private IUnitOfWork Database { get; set; }

        public FileToDownloadService(IUnitOfWork uow)
        {
            Database = uow;
            _mapper = new Mapper(MapperConfigBL.Configure());
        }

        public IEnumerable<FileToDownloadDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<FilesToDownload>, IEnumerable<FileToDownloadDTO>>(Database.FilesToDownload.Get());
        }

        public FileToDownloadDTO FindById(int id)
        {
            return _mapper.Map<FilesToDownload, FileToDownloadDTO>(Database.FilesToDownload.Get(x => x.Id.Equals(id)));
        }

        public void Add(FileToDownloadDTO fileToDownloadDTO)
        {
            var file = _mapper.Map<FileToDownloadDTO, FilesToDownload>(fileToDownloadDTO);
            Database.FilesToDownload.Add(file);
            Database.Save();
        }

        public void Delete(int id)
        {
            var file = Database.FilesToDownload.Get(x => x.Id.Equals(id));
            Database.FilesToDownload.Delete(file);
            Database.Save();
        }

        public void Update(FileToDownloadDTO fileToDownloadDTO)
        {
            var file = _mapper.Map<FileToDownloadDTO, FilesToDownload>(fileToDownloadDTO);
            Database.FilesToDownload.Update(file);
            Database.Save();
        }
    }
}
