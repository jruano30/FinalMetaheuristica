using GeneticAlgorithm.Interfaces;
using GeneticAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.MotherNature
{
    public class MotherNature : IMotherNature
    {
        private readonly DarwinBase darwin;

        private readonly IGod god;

        private IEnumerable<IBe> population;

        private int currentIteration;

        private IRandomOperations randomOperation;

        private IEqualityComparer<IBe> comparer;

        public MotherNature(IEnumerable<IBe> initialPopulation, DarwinBase darwin, IGod god, IRandomOperations randomOperation, IEqualityComparer<IBe> comparer)
        {
            this.darwin = darwin;
            population = initialPopulation;
            this.god = god;
            this.randomOperation = randomOperation;
            this.comparer = comparer;
        }

        public IEnumerable<IBe> Evolve(Action<IEnumerable<IBe>, int> perIteractionAction)
        {
            currentIteration = 0;
            while (currentIteration <= god.maximunIteration)
            {
                List<IBe> currentPopulation = new List<IBe>();
                                
                for (int i = 0; i < god.maximunOffspring; i++)
                {
                    var operation = randomOperation.GetOperation();

                    switch (operation)
                    {
                        case Operations.Mutation:
                            currentPopulation = currentPopulation.Concat(darwin.Mutate(god.GetSingle(population, i))).ToList();
                            break;
                        case Operations.CrossOver:
                            var couple = god.GetCouple(population, i);
                            currentPopulation = currentPopulation.Concat(darwin.Conceive(couple.Item1, couple.Item2)).ToList();
                            break;
                    }
                }

                population = population.Union(darwin.CalculateSolution(currentPopulation)).OrderBy(n => -n.Solution).Distinct(comparer).Take(god.maximunOffspring).ToList();

                if (perIteractionAction != null) perIteractionAction.Invoke(population, currentIteration);

                currentIteration++;
            }

            return population;
        }
    }
}
