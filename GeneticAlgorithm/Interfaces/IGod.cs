using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Interfaces
{
    public interface IGod
    {
        int maximunIteration { get; set; }

        int maximunOffspring { get; set; }

        IBe GetSingle(IEnumerable<IBe> pupulation, int index);

        Tuple<IBe,IBe> GetCouple(IEnumerable<IBe> pupulation, int index);
    }
}
