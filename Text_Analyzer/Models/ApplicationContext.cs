using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Text_Analyzer.Models
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<UploadedFile> UploadedFiles { get; set; }
        public virtual DbSet<FileToDownload> FileToDownloads { get; set; }
        public virtual DbSet<FileLinks> FileLinks { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }


    }
}
