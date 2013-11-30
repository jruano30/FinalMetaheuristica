using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Interfaces
{
    public interface IConceiver
    {
        IEnumerable<IBe> Conceive(IBe father, IBe mother);
    }
}
