using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task2.Models;
using Text_Analyzer.Models;
using Text_Analyzer.TextUtility.DataTransferObject;

namespace Text_Analyzer.Utils
{
    public class MapperWebConfig : Profile
    {
        public MapperWebConfig()
        {
            CreateMap<ConcordanceItemsDTO, ConcordanceItemViewModel>().ReverseMap();
        }
    }
}
