using GeneticAlgorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.MotherNature
{
    class MotherNature : IMotherNature
    {
        private readonly DarwinBase darwin;

        private readonly IGod god;

        private IEnumerable<IBe> population;

        private int currentIteration;
        
        public MotherNature(IEnumerable<IBe> initialPopulation, DarwinBase darwin, IGod god)
        {
            this.darwin = darwin;
            population = initialPopulation;
            this.god = god;
        }

        public IEnumerable<IBe> Evolve()
        {
            currentIteration = 0;
            while (currentIteration <= god.maximunIteration)
            {
                List<IBe> currentPopulation = new List<IBe>();

                Parallel.For(0, god.maximunOffspring, (i) =>
                {
                    if ((i + DateTime.Now.Ticks) % 2 == 0)
                    {
                        currentPopulation.Add(darwin.Mutate(god.GetSingle(population, i)));
                    }
                    else
                    {
                        var couple = god.GetCouple(population, i);
                        currentPopulation.Add(darwin.Conceive(couple.Item1, couple.Item2));
                    }
                });
                
                population = population.Union(darwin.CalculateSolution(currentPopulation)).OrderBy(n => n.Solution).Take(god.maximunOffspring);
                
                currentIteration++;           
            }
            
            return population;
        }
    }
}
