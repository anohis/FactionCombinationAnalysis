using System;
using System.Collections.Generic;
using System.Text;

namespace FactionCombinationAnalysis.Apriori
{
    public interface IAprioriData<T>
    {
        IEnumerable<T> GetData();
    }
}
