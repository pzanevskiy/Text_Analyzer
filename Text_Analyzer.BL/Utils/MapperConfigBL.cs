using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.BL.DTO;
using Text_Analyzer.DB;

namespace Text_Analyzer.BL.Utils
{
    public class MapperConfigBL
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration
            (
                cfg =>
                {
                    cfg.CreateMap<UploadedFiles, UploadedFileDTO>().ReverseMap();
                    cfg.CreateMap<FilesToDownload, FileToDownloadDTO>().ReverseMap();
                    cfg.CreateMap<FileLinks, FileLinksDTO>()
                    .ForMember("UploadedFiles", opt => opt.MapFrom(x => x.UploadedFiles.Filename))
                    .ForMember("FilesToDownload", opt => opt.MapFrom(x => x.FilesToDownload.Filename));
                }
            );
            return config;
        }
    }
}
