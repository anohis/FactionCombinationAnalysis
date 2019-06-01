using FactionCombinationAnalysis.Faction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactionCombinationAnalysis.FactionCombine.Combiner
{
    public abstract class CombinerBase
    {
        public IEnumerable<FactionType> GetCombination()
        {
            var list = CreateCombination();
            if (list.Count() != Consts.CombineMax)
            {
                throw new Exception(string.Format("[CombinerBase.GetCombination] list count {0}", list.Count()));
            }
            return list;
        }

        protected abstract IEnumerable<FactionType> CreateCombination();
    }
}
