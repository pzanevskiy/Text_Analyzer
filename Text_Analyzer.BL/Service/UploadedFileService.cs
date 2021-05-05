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
    public class UploadedFileService : IUploadedFileService
    {
        private IMapper _mapper;

        private IUnitOfWork Database { get; set; }

        public UploadedFileService(IUnitOfWork uow)
        {
            Database = uow;
            _mapper = new Mapper(MapperConfigBL.Configure());
        }

        public IEnumerable<UploadedFileDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<UploadedFiles>, IEnumerable<UploadedFileDTO>>(Database.UploadedFiles.Get());
        }

        public UploadedFileDTO FindById(int id)
        {
            return _mapper.Map<UploadedFiles, UploadedFileDTO>(Database.UploadedFiles.Get(x => x.Id.Equals(id)));   
        }

        public void Add(UploadedFileDTO uploadedFileDTO)
        {
            var file = _mapper.Map<UploadedFileDTO, UploadedFiles>(uploadedFileDTO);
            Database.UploadedFiles.Add(file);
            Database.Save();
        }

        public void Delete(int id)
        {
            var file = Database.UploadedFiles.Get(x => x.Id.Equals(id));
            Database.UploadedFiles.Delete(file);
            Database.Save();
        }

        public void Update(UploadedFileDTO uploadedFileDTO)
        {
            var file = _mapper.Map<UploadedFileDTO, UploadedFiles>(uploadedFileDTO);
            Database.UploadedFiles.Update(file);
            Database.Save();
        }
    }
}
