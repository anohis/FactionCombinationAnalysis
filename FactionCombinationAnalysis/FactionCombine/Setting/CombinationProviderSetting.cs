using FactionCombinationAnalysis.FactionCombine.Combiner;
using System;
using System.Collections.Generic;
using System.Text;

namespace FactionCombinationAnalysis.FactionCombine
{
    public class CombinationProviderSetting
    {
        public Dictionary<CombinerBase, double> Combiners = new Dictionary<CombinerBase, double>();
    }
}
