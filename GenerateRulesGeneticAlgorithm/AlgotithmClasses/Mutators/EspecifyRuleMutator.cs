using GenerateRulesGeneticAlgorithm.Model;
using GeneticAlgorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.AlgotithmClasses.Mutators
{
    public class EspecifyRuleMutator : IMutator
    {
        public IEnumerable<IEnumerable<char>> posibleValues { get; set; }

        public EspecifyRuleMutator(IEnumerable<IEnumerable<char>> maxValues)
        {
            posibleValues = maxValues;
        }

        public IEnumerable<IBe> Mutate(IBe be)
        {
            var ruleValues = (be as MushroomRule).Rule.ToString().Split(',');

            var random = new Random();
            var indexToSpecify = random.Next(0, ruleValues.Count() - 1);

            var attempts = 10;

            while (attempts > 0)
            {
                if (ruleValues.ElementAt(indexToSpecify)[0] == '0')
                {
                    var pvalues = posibleValues.ElementAt(indexToSpecify);
                    var index = random.Next(pvalues.Count() - 1);
                    ///TODO: remove index
                    ruleValues.SetValue(pvalues.ElementAt(index).ToString(), indexToSpecify);
                    break;
                }
                indexToSpecify = random.Next(0, ruleValues.Count() - 1);
                attempts--;
            }

            return new[]
            {
                new MushroomRule((be as MushroomRule).DataProvider, be.Generation + 1, new Mushroom(ruleValues), (be as MushroomRule).ClassValue)
            };
        }
    }
}
