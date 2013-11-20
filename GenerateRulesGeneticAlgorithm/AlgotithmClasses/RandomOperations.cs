using GeneticAlgorithm.Interfaces;
using GeneticAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.AlgotithmClasses
{
    public class RandomOperations : IRandomOperations
    {
        public Operations GetOperation()
        {
            var random = new Random();

            if (random.NextDouble() > 0.2)
            {
                return Operations.CrossOver;
            }
            else
            {
                return Operations.Mutation;
            }
        }
    }
}
