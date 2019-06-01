using FactionCombinationAnalysis.FactionCombine.Combiner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactionCombinationAnalysis.FactionCombine
{
    public class CombinationProvider
    {
        public static CombinationProvider Instance { get; private set; }

        private Dictionary<CombinerBase, double> _combiners;
        private Random _random;

        public static void Initialize(CombinationProviderSetting setting)
        {
            Instance = new CombinationProvider(setting);
        }
        private CombinationProvider(CombinationProviderSetting setting)
        {
            _random = new Random();
            _combiners = setting.Combiners;
        }

        public Combination CreateCombination()
        {
            var combinationA = RandomCombiner().GetCombination();
            var combinationB = RandomCombiner().GetCombination();

            return new Combination(combinationA, combinationB);
        }

        private CombinerBase RandomCombiner()
        {
            CombinerBase combiner = null;

            double random = _random.NextDouble();
            foreach (var v in _combiners)
            {
                random -= v.Value;
                if (random <= 0)
                {
                    combiner = v.Key;
                    break;
                }
            }

            return combiner;
        }
    }
}
