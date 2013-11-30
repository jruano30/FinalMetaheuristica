using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Interfaces
{
    public class DarwinBase
    {
        IEnumerable<IMutator> mutators;

        IEnumerable<IConceiver> conceivers;

        public DarwinBase(IEnumerable<IMutator> mutators, IEnumerable<IConceiver> conceivers)
        {
            this.mutators = mutators;
            this.conceivers = conceivers;
        }

        public IEnumerable<IBe> Mutate(IBe xBe)
        {
            var random = new Random();

            return mutators.ElementAt(random.Next(0, mutators.Count())).Mutate(xBe);
            
        }

        public IEnumerable<IBe> Conceive(IBe father, IBe mother)
        {
            var random = new Random();
            return conceivers.ElementAt(random.Next(0, conceivers.Count())).Conceive(father, mother);
        }

        public IEnumerable<IBe> CalculateSolution(IEnumerable<IBe> population)
        {
            foreach (var be in population)
            {
                be.CalcularSolucion();
                yield return be;
            }
        }
    }
}
