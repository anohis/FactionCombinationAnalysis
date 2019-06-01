using System;
using System.Collections.Generic;
using System.Text;
using FactionCombinationAnalysis.FactionCombine.Combiner;

namespace FactionCombinationAnalysis.FactionCombine.Setting
{
    public class CombinationProviderSetting_A : CombinationProviderSetting
    {
        public CombinationProviderSetting_A()
        {
            Combiners.Add(new Combiner_A(), 0.5);
            Combiners.Add(new Combiner_B(), 0.5);
        }
    }
}
