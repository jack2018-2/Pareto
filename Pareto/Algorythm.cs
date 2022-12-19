using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pareto
{
    public class Algorythm
    {
        private Dictionary<int, List<int>> _idxToLines;
        private Dictionary<int, bool> _dominatedToIdx;

        public Algorythm()
        {
            _idxToLines = new Dictionary<int, List<int>>();
            _dominatedToIdx = new Dictionary<int, bool>();
        }

        public void Solve()
        {
            using (StreamReader sr = new StreamReader("C:/Users/jack2/source/repos/Pareto/Pareto/data.csv"))
            {
                string currentLine;
                var idx = 0;
                while ((currentLine = sr.ReadLine()) != null)
                {
                    var thisLine = currentLine.Split(",\t").Select(x => int.Parse(x));

                    _idxToLines.Add(idx, thisLine.ToList());
                    _dominatedToIdx.Add(idx, false);

                    idx++;
                }
            }

            for (var i = 0; i < _idxToLines.Values.Count; i++)
            {
                for (var j = 0; j < _idxToLines.Values.Count && j != i; j++)
                {
                    if (j == i) continue;
                    var isDominated = isFirstDominatedBySecond(_idxToLines[i], _idxToLines[j]);
                    _dominatedToIdx[i] = _dominatedToIdx[i] || isDominated;
                }
            }

            for (var i = 0; i < _idxToLines.Values.Count; i++)
            {
                var isDominated = _dominatedToIdx[i];

                Console.WriteLine(string.Join(",", _idxToLines[i].Select(x => x.ToString())) + (isDominated ? " - 0 " : " - 1 "));

            }
        }

        /// <summary>
        /// Доминировние второй над первой
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private bool isFirstDominatedBySecond(List<int> first, List<int> second)
        {
            var result = true;
            for (var i = 0; i < first.Count(); i++)
            {
                if (first[i] >= second[i])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
