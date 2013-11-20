using GenerateRulesGeneticAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm.MotherNature;

namespace FinalMetaheuristica
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Mushroom> mushroms = ReadCsv().ToList();

            Console.Write(mushroms.ElementAt(0));
            Console.ReadKey();

            // var mn = new MotherNature();

            Console.ReadKey();
        }

        private static IEnumerable<Mushroom> ReadCsv()
        {
            var reader = new StreamReader(File.OpenRead("agaricus-lepiota.data"));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                yield return new Mushroom(values);
            }
        }
    }
}
