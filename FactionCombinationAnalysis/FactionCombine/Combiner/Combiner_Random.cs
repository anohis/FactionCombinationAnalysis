using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FactionCombinationAnalysis.Faction;

namespace FactionCombinationAnalysis.FactionCombine.Combiner
{
    public class Combiner_Random : CombinerBase
    {
        private Random _random = new Random();

        protected override IEnumerable<FactionType> CreateCombination()
        {
            var list = Enum.GetValues(typeof(FactionType)).Cast<FactionType>().ToList();
            var selectList = new HashSet<FactionType>();

            for (int i = 0; i < Consts.CombineMax; i++)
            {
                var select = RandomFaction(list);
                list.Remove(select);
                selectList.Add(select);
            }

            return selectList;
        }

        private FactionType RandomFaction(IEnumerable<FactionType> list)
        {
            int index = _random.Next(0, list.Count());

            return list.ElementAt(index);
        }
    }
}
