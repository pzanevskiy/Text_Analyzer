using System;
using Text_Analyzer.DAL.Repository;
using Text_Analyzer.DB;

namespace Text_Analyzer.DAL.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<UploadedFiles> UploadedFiles { get; }
        IGenericRepository<FilesToDownload> FilesToDownload { get; }
        IGenericRepository<FileLinks> FileLinks { get; }

        void Save();
    }
}
