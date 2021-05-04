using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Text_Analyzer.Utility.Models.Interfaces;

namespace Text_Analyzer.Utility.Comparer
{
    public class WordComparer : IEqualityComparer<IWord>
    {
        public bool Equals([AllowNull] IWord x, [AllowNull] IWord y)
        {
            if (Object.ReferenceEquals(x, y)) return true;
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;
            return x.ToString().ToLower() == y.ToString().ToLower();
        }

        public int GetHashCode([DisallowNull] IWord obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashWord = obj.ToString().ToLower() == null ? 0 : obj.ToString().ToLower().GetHashCode();

            return hashWord;
        }
    }
}
