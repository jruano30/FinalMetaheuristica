using GeneticAlgorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.AlgotithmClasses
{
    public class DarwinRules : DarwinBase
    {
        public DarwinRules(IEnumerable<IMutator> mutators, IEnumerable<IConceiver> conceivers)
            : base(mutators, conceivers)
        {
        }
    }
}
