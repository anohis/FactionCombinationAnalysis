using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FactionCombinationAnalysis.Apriori
{
    internal class SetData<T>
    {
        public HashSet<T> Set = new HashSet<T>();
        public T Select
        {
            get { return _select; }
            set
            {
                _hasSelect = true;
                _select = value;
            }
        }
        public double Support;
        public double Confidence;
        public double Lift;
        private bool _hasSelect = false;
        public IEnumerable<T> FullSet
        {
            get
            {
                var clone = new HashSet<T>(Set);
                if (_hasSelect)
                {
                    clone.Add(Select);
                }
                return clone;
            }
        }

        private T _select;

        public override string ToString()
        {
            string setString = "{";
            foreach (T item in Set)
            {
                setString += item.ToString() + ",";
            }
            setString += "}";

            if (_hasSelect)
            {
                setString += " => " + Select.ToString();
            }

            return string.Format("{0}\tSupport:\t{1:0.000}\tConfidence:\t{2:0.000}\tLift:\t{3:0.000}", setString, Support, Confidence, Lift);
        }
    }
}