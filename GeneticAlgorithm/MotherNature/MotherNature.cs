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

        public MotherNature(IEnumerable<IBe> initialPopulation, DarwinBase darwin, IGod god, IRandomOperations randomOperation)
        {
            this.darwin = darwin;
            population = initialPopulation;
            this.god = god;
            this.randomOperation = randomOperation;
        }

        public IEnumerable<IBe> Evolve(Action<IEnumerable<IBe>> perIteractionAction)
        {
            currentIteration = 0;
            while (currentIteration <= god.maximunIteration)
            {
                List<IBe> currentPopulation = new List<IBe>();

                Parallel.For(0, god.maximunOffspring, (i) =>
                {
                    var operation = randomOperation.GetOperation();

                    switch (operation)
                    {
                        case Operations.Mutation:
                            currentPopulation.Add(darwin.Mutate(god.GetSingle(population, i)));
                            break;
                        case Operations.CrossOver:
                            var couple = god.GetCouple(population, i);
                            currentPopulation.Add(darwin.Conceive(couple.Item1, couple.Item2));
                            break;
                    }
                });

                population = population.Union(darwin.CalculateSolution(currentPopulation)).OrderBy(n => n.Solution).Take(god.maximunOffspring);

                if(perIteractionAction != null) perIteractionAction.Invoke(population);

                currentIteration++;
            }

            return population;
        }
    }
}
