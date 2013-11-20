using GenerateRulesGeneticAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.Interfaces
{
    public interface IDataProvider
    {
        IEnumerable<Mushroom> GetData();
    }
}
