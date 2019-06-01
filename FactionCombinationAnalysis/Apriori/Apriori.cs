using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace FactionCombinationAnalysis.Apriori
{
    public class Apriori<T>
    {
        private List<IEnumerable<T>> _datas;
        private double _minSupport;
        private double _minConfidence;

        private List<SetData<T>> _setDatas;
        private Dictionary<T, double> _supportTable;

        public Apriori(double minSupport,double minConfidence, IEnumerable<IAprioriData<T>> datas)
        {
            _minSupport = minSupport;
            _minConfidence = minConfidence;

            _datas = new List<IEnumerable<T>>();
            _setDatas = new List<SetData<T>>();
            _supportTable = new Dictionary<T, double>();

            var itemHashset = new HashSet<T>();
            foreach (var data in datas)
            {
                var list = data.GetData();
                _datas.Add(list);

                foreach (T item in list)
                {
                    itemHashset.Add(item);
                }
            }

            foreach (T item in itemHashset)
            {
                SetData<T> set = new SetData<T>();
                set.Set.Add(item);
                if (CheckSupport(set))
                {
                    _setDatas.Add(set);
                }
                _supportTable.Add(item, set.Support);
            }
        }

        public void Train()
        {
            Show();

            List<SetData<T>> newSetDatas = new List<SetData<T>>();

            Dictionary<IEnumerable<T>, T> exist = new Dictionary<IEnumerable<T>, T>();

            for (int i = 0; i < _setDatas.Count; i++)
            {
                for (int j = 0; j < _setDatas.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var fullSetI = _setDatas[i].FullSet;
                    var fullSetJ = _setDatas[j].FullSet;
                    var union = fullSetI.Union(fullSetJ);

                    if (union.Count() == fullSetI.Count() + 1)
                    {
                        var select = union.Except(fullSetI).ElementAt(0);

                        bool hasSameSet = false;
                        foreach (var v in exist)
                        {
                            if (v.Key.Except(fullSetI).Count() == 0 
                                && select.Equals(v.Value))
                            {
                                hasSameSet = true;
                                break;
                            }
                        }

                        if (hasSameSet)
                        {
                            continue;
                        }


                        SetData<T> set = new SetData<T>();
                        set.Set = new HashSet<T>(fullSetI);
                        set.Select = select;
                        set.Confidence = _setDatas[i].Support;

                        if (CheckSupport(set)
                            && CheckConfidence(set))
                        {
                            set.Lift = set.Confidence / _supportTable[select];

                            newSetDatas.Add(set);
                            exist.Add(fullSetI, select);
                        }
                    }
                }
            }

            if (newSetDatas.Count > 0)
            {
                _setDatas = newSetDatas;
                Train();
            }
        }

        private bool CheckSupport(SetData<T> setData)
        {
            var set = setData.FullSet;

            double count = 0;
            foreach (var data in _datas)
            {
                if (data.Intersect(set).Count() == set.Count())
                {
                    count++;
                }
            }
            setData.Support = count / _datas.Count;

            return setData.Support >= _minSupport;
        }
        private bool CheckConfidence(SetData<T> setData)
        {
            setData.Confidence = setData.Support / setData.Confidence;
            return setData.Confidence >= _minConfidence;
        }
        private void Show()
        {
            _setDatas.Sort((a, b) => { return Math.Sign(b.Confidence - a.Confidence); });

            foreach (var setData in _setDatas)
            {
                Console.WriteLine(setData.ToString());
            }
        }
    }
}
