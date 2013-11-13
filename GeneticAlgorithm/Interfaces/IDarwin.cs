using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Interfaces
{
    public interface IDarwin
    {
        IBe Mutate(IBe XBe);

        IBe Conceive(IBe Father, IBe Mother);

        IEnumerable<IBe> CalculateSolution(IEnumerable<IBe> population);
    }
}
