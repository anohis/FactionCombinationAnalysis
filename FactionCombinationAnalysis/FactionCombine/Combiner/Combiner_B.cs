﻿using FactionCombinationAnalysis.Faction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FactionCombinationAnalysis.FactionCombine.Combiner
{
    class Combiner_B : CombinerBase
    {
        private Random _random = new Random();
        private Dictionary<FactionType, List<FactionType>> _strategyTable;

        public Combiner_B()
        {
            _strategyTable = new Dictionary<FactionType, List<FactionType>>();

            _strategyTable.Add(FactionType.A, new List<FactionType>()
            {
                FactionType.B,
            });
            _strategyTable.Add(FactionType.B, new List<FactionType>()
            {
                FactionType.C,
            });
            _strategyTable.Add(FactionType.D, new List<FactionType>()
            {
                FactionType.E,
            });
            _strategyTable.Add(FactionType.E, new List<FactionType>()
            {
                FactionType.F,
            });
        }

        protected override IEnumerable<FactionType> CreateCombination()
        {
            var list = Enum.GetValues(typeof(FactionType)).Cast<FactionType>().ToList();
            var selectList = new HashSet<FactionType>();

            while (selectList.Count < Consts.CombineMax)
            {
                var select = RandomFaction(list);
                list.Remove(select);
                selectList.Add(select);

                FactionType strategy;
                while (CheckStrategy(selectList, select, out strategy))
                {
                    select = strategy;
                    list.Remove(strategy);
                    selectList.Add(strategy);
                }
            }

            return selectList;
        }

        private bool CheckStrategy(IEnumerable<FactionType> selectList, FactionType select, out FactionType strategy)
        {
            strategy = FactionType.A;

            if (selectList.Count() < Consts.CombineMax
                && _strategyTable.ContainsKey(select))
            {
                var list = _strategyTable[select].Except(selectList);
                if (list.Count() > 0)
                {
                    strategy = RandomFaction(list);
                    return true;
                }
            }

            return false;
        }

        private FactionType RandomFaction(IEnumerable<FactionType> list)
        {
            int index = _random.Next(0, list.Count());

            return list.ElementAt(index);
        }
    }
}
