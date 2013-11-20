using GenerateRulesGeneticAlgorithm.Interfaces;
using GenerateRulesGeneticAlgorithm.Model;
using GeneticAlgorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.AlgotithmClasses
{
    public class MushroomRule : IBe
    {
        public readonly IDataProvider dataProvider;

        public Mushroom Rule { get; set; }

        public MushroomRule(IDataProvider dataProvider, int generacion)
        {
            this.dataProvider = dataProvider;
            Generation = generacion;
        }

        public double Solution { get; set; }

        public int Generation { get; set; }

        public void CalcularSolucion()
        {
            Solution = dataProvider.GetData().Count(m => m.IsEqual(Rule)) / dataProvider.GetData().Count();
        }
    }
}
