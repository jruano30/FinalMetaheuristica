using GeneticAlgorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.AlgotithmClasses
{
    public class God : IGod
    {

        public int maximunIteration {get; set;}

        public int maximunOffspring { get; set; }

        public IBe GetSingle(IEnumerable<IBe> population, int index)
        {
            return population.ElementAt(index);
        }

        public Tuple<IBe, IBe> GetCouple(IEnumerable<IBe> population, int index)
        {
            var random = new Random();
            var randomIndex = random.Next(0, population.Count());

            return new Tuple<IBe, IBe>(GetSingle(population, index), population.ElementAt(randomIndex));
        }
    }
}
