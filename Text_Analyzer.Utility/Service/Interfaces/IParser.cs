using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Text_Analyzer.Utility.Models.Interfaces;

namespace Text_Analyzer.Utility.Service.Interfaces
{
    public interface IParser
    {
        public IText ParseText(StreamReader reader);
        public IText ParseText(ICollection<string> strings);

    }
}
