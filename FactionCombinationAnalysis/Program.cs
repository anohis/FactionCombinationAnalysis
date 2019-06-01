using FactionCombinationAnalysis.Apriori;
using FactionCombinationAnalysis.Faction;
using FactionCombinationAnalysis.FactionCombine;
using FactionCombinationAnalysis.FactionCombine.Setting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FactionCombinationAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            CombinationProvider.Initialize(new CombinationProviderSetting_A());

            var list = new List<Combination>();
            for (int i = 0; i < 10000; i++)
            {
                var combination = CombinationProvider.Instance.CreateCombination();
                list.Add(combination);
                //Console.WriteLine(combination.ToString());
            }

            Console.WriteLine("===============================");
            new Apriori<FactionType_TwoPlayer>(0.25, 0, list.Cast<IAprioriData<FactionType_TwoPlayer>>()).Train();

            Console.WriteLine("End");
            Console.Read();
        }
    }
}
