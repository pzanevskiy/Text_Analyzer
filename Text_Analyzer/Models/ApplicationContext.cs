using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Text_Analyzer.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UploadedFile> UploadedFiles { get; set; }
        public DbSet<FileToDownload> FileToDownloads { get; set; }
        public DbSet<FileLinks> FileLinks { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}
