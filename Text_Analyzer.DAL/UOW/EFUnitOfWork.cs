using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.DAL.Context;
using Text_Analyzer.DAL.Repository;
using Text_Analyzer.DB;

namespace Text_Analyzer.DAL.UOW
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TextAnalyzerContext _context;
        private IGenericRepository<UploadedFiles> _uploadedFilesRepository;
        private IGenericRepository<FilesToDownload> _filesToDownloadRepository;
        private IGenericRepository<FileLinks> _fileLinksRepository;

        public EFUnitOfWork()
        {
            _context = new TextAnalyzerContext();
        }

        public IGenericRepository<UploadedFiles> UploadedFiles
        {
            get
            {
                if (_uploadedFilesRepository == null)
                {
                    _uploadedFilesRepository = new GenericRepository<UploadedFiles>(_context);
                }
                return _uploadedFilesRepository;
            }
        }

        public IGenericRepository<FilesToDownload> FilesToDownload
        {
            get
            {
                if (_filesToDownloadRepository == null)
                {
                    _filesToDownloadRepository = new GenericRepository<FilesToDownload>(_context);
                }
                return _filesToDownloadRepository;
            }
        }

        public IGenericRepository<FileLinks> FileLinks
        {
            get
            {
                if (_fileLinksRepository == null)
                {
                    _fileLinksRepository = new GenericRepository<FileLinks>(_context);
                }
                return _fileLinksRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
