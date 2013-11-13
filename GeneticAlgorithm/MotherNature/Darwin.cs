using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.MotherNature
{
    public class Darwin : IDarwin
    {
        private IEnumerable<IMutator> Mutators;

        private IEnumerable<IConceiver> Conceivers;
        
        public IBe Mutate(IBe XBe)
        {
            throw new NotImplementedException();
        }

        public IBe Conceive(IBe Father, IBe Mother)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<IBe> CalculateSolution(IEnumerable<IBe> population)
        {
            throw new NotImplementedException();
        }
    }
}
