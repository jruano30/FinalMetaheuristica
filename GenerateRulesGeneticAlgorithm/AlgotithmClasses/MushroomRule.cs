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
        private double? solution;

        public IDataProvider DataProvider { get; set; }

        public Mushroom Rule { get; set; }

        public char ClassValue { get; set; }

        public MushroomRule(IDataProvider dataProvider, int generacion, Mushroom mushroom, char classValue)
            : this(dataProvider, generacion, classValue)
        {
            this.Rule = mushroom;          
        }

        public MushroomRule(IDataProvider dataProvider, int generacion, char classValue)
        {
            DataProvider = dataProvider;
            Generation = generacion;
            ClassValue = classValue;
            solution = null;  
        }

        public double Solution
        {
            get
            {
                if (!solution.HasValue)
                {
                    CalcularSolucion();
                }
                return solution.Value;
            }
        }

        public int Generation { get; set; }

        public void CalcularSolucion()
        {
            if (!solution.HasValue)
            {
                double positiveTrue = DataProvider.GetData().Count(m => m.IsEqual(Rule) && m.classValue == ClassValue);
                double down = DataProvider.GetData().Count(m => m.IsEqual(Rule));
                if (down == 0)
                {
                    solution = 0;
                }
                else
                {
                    solution = (positiveTrue) / down;
                }
            }
        }

        public override bool Equals(object obj)
        {
            var otherRule = obj as MushroomRule;

            if (otherRule == null) return false;

            if (Rule == null && otherRule.Rule == null) return true;

            return Rule.IsEqual(otherRule.Rule);
        } 
    }
}
