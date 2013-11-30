using GeneticAlgorithm.Interfaces;
using GenerateRulesGeneticAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.AlgotithmClasses.Mutators
{
    public class GeneralizeRuleMutator : IMutator
    {
        public IEnumerable<IBe> Mutate(IBe be)
        {
            var ruleValues = (be as MushroomRule).Rule.ToString().Split(',');

            var random = new Random();
            var indexToGeneralize = random.Next(0, ruleValues.Count());

            var attempts = 10;

            while (attempts > 0)
            {
                if (ruleValues.ElementAt(indexToGeneralize)[0] != '0')
                {
                    ruleValues.SetValue(ruleValues.ElementAt(indexToGeneralize), indexToGeneralize);
                    break;
                }

                indexToGeneralize = random.Next(0, ruleValues.Count());
                attempts--;
            }

            return new[]
            {
                new MushroomRule((be as MushroomRule).DataProvider, be.Generation + 1, new Mushroom(ruleValues), (be as MushroomRule).ClassValue)
            };
        }
    }
}
