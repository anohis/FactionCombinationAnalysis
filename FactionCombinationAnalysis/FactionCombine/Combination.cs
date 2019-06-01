using FactionCombinationAnalysis.Apriori;
using FactionCombinationAnalysis.Faction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactionCombinationAnalysis.FactionCombine
{
    public struct Combination : IAprioriData<FactionType_TwoPlayer>
    {
        public IEnumerable<FactionType> CombinationA { get; private set; }
        public IEnumerable<FactionType> CombinationB { get; private set; }

        public Combination(IEnumerable<FactionType> a, IEnumerable<FactionType> b)
        {
            CombinationA = new HashSet<FactionType>(a);
            CombinationB = new HashSet<FactionType>(b);
        }

        public IEnumerable<FactionType_TwoPlayer> GetData()
        {
            var list = new List<FactionType_TwoPlayer>();
            list.AddRange(CombinationA.Cast<FactionType_TwoPlayer>());
            for (int i = 0; i < list.Count; i++)
            {
                list[i] += (int)FactionType_TwoPlayer.a;
            }
            list.AddRange(CombinationB.Cast<FactionType_TwoPlayer>());
            return list;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", ToString(CombinationA), ToString(CombinationB));
        }

        private string ToString<T>(IEnumerable<T> list)
        {
            string setString = "{";
            foreach (T item in list)
            {
                setString += item.ToString() + ",";
            }
            setString += "}";
            return setString;
        }
    }
}
