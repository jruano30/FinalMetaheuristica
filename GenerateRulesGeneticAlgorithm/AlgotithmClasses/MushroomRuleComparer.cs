using GeneticAlgorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.AlgotithmClasses
{
    public class MushroomRuleComparer : IEqualityComparer<IBe>
    {
        public bool Equals(IBe x, IBe y)
        {
            var xMR = x as MushroomRule;
            var yMR = y as MushroomRule;

            if (xMR == null && yMR == null) return true;
            if (xMR == null || yMR == null) return false;

            return xMR.Equals(yMR);
        }

        public int GetHashCode(IBe obj)
        {
            var objMushroom = obj as MushroomRule;
            if (objMushroom == null) return -1;

            var hash = 0;

            foreach (var character in objMushroom.Rule.ToString())
            {
                hash += ((int)character);
            }

            return hash;
        }
    }
}
