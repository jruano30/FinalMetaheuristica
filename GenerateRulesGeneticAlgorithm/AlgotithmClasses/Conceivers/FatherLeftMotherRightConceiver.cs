using GenerateRulesGeneticAlgorithm.Model;
using GeneticAlgorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.AlgotithmClasses.Conceivers
{
    public class FatherLeftMotherRightConceiver : IConceiver
    {
        public IEnumerable<IBe> Conceive(IBe father, IBe mother)
        {
            var mushroomRuleFatherRuleString = (father as MushroomRule).Rule.ToString().Split(',');
            var mushroomRuleMotherRuleString = (mother as MushroomRule).Rule.ToString().Split(',');

            var random = new Random();

            var splitIndex = random.Next(0, mushroomRuleFatherRuleString.Count());

            return new[]
            {
                new MushroomRule((father as MushroomRule).DataProvider, Math.Max(father.Generation, mother.Generation) + 1, (father as MushroomRule).ClassValue) 
                {
                    Rule = new Mushroom(mushroomRuleFatherRuleString.Take(splitIndex).Concat(mushroomRuleMotherRuleString.Skip(splitIndex)))
                }
            };
        }
    }
}
