using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Text_Analyzer.Utility.DataTransferObject;
using Text_Analyzer.Utility.Models;

namespace Text_Analyzer.Utility.Utils
{
    public class MapperConfig
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration
            (
                cfg =>
                {
                    cfg.CreateMap<ConcordanceItem, ConcordanceItemsDTO>().ReverseMap();
                }
            );
            return config;
        }
    }
}
