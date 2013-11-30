using GenerateRulesGeneticAlgorithm.Interfaces;
using GenerateRulesGeneticAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.Providers
{
    public class RulesProvider : IDataProvider
    {
        public IEnumerable<Mushroom> mushrooms;

        public RulesProvider(IEnumerable<Mushroom> mushrooms)
        {
            this.mushrooms = mushrooms;
        }

        public IEnumerable<Mushroom> GetData()
        {
            return mushrooms;
        }
    }
}
