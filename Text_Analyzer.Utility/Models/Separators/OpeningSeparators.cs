using System;
using System.Collections.Generic;
using System.Text;

namespace Text_Analyzer.Utility.Models.Separators
{
    public class OpeningSeparators : Separator
    {
        string[] openingSeparators = { "(", "[", "{", "«", "“", "‘" };

        public override string[] GetSeparators()
        {
            return openingSeparators;
        }
    }
}
