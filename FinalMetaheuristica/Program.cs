using GenerateRulesGeneticAlgorithm.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneticAlgorithm.MotherNature;
using GenerateRulesGeneticAlgorithm.AlgotithmClasses;
using GenerateRulesGeneticAlgorithm.Providers;
using GeneticAlgorithm.Interfaces;
using GenerateRulesGeneticAlgorithm.AlgotithmClasses.Mutators;
using GenerateRulesGeneticAlgorithm.AlgotithmClasses.Conceivers;
using System.Reflection;
using GenerateRulesGeneticAlgorithm;
using System.Diagnostics;

namespace FinalMetaheuristica
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Mushroom> mushromsAll = ReadCsv().OrderBy(m => new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0)).NextDouble()).ToList();

            var trainigPercentage = 0.8;

            var mushromsTraining = mushromsAll.Take(((int)Math.Round(mushromsAll.Count() * trainigPercentage, 0)));
            var mushromsTest = mushromsAll.Take(((int)Math.Round(mushromsAll.Count() * (1 - trainigPercentage), 0)));

            Console.WriteLine("Initial Rules");
            var watch = Stopwatch.StartNew();
            var controller = new GeneticsAlogorithmController();

            var initialPopulation = 500;
            var iterations = 100;
            var offspring = 500;

            var generatePRules = new Task<IEnumerable<MushroomRule>>(() =>
            {
                return controller.GenerateRules(mushromsTraining, 'p', initialPopulation, iterations, offspring, (rulesP, iteration) =>
                {
                    var best = rulesP.OrderBy(m => -m.Generation - m.Solution).FirstOrDefault();
                    Console.Write(string.Format("\rP->{0}/{1} S = {2}", best.Generation, iteration, best.Solution));
                });
            });

            var generateERules = new Task<IEnumerable<MushroomRule>>(() =>
            {
                return controller.GenerateRules(mushromsTraining, 'e', initialPopulation, iterations, offspring, (rulesP, iteration) =>
                {
                    var best = rulesP.OrderBy(m => -m.Generation - m.Solution).FirstOrDefault();
                    Console.Write(string.Format("\rE->{0}/{1} S = {2}", best.Generation, iteration, best.Solution));
                });
            });

            generatePRules.Start();
            generateERules.Start();
            var pRules = generatePRules.Result;
            var eRules = generateERules.Result;
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("");
            Console.WriteLine(string.Format("Final Rules {0}", elapsedMs));

            IEnumerable<Mushroom> resultTestP = EvaluatePopulation(mushromsTest, pRules);

            double vP = resultTestP.Where(m => m.classValue == 'p').Count();
            double fP = mushromsTest.Except(resultTestP).Where(m => m.classValue != 'p').Count();

            var accuracyP = (vP + fP) / mushromsTest.Count();

            Console.WriteLine(string.Format("P Accuracy : {0}", accuracyP));

            IEnumerable<Mushroom> resultTestE = EvaluatePopulation(mushromsTest, eRules);

            double vE = resultTestE.Where(m => m.classValue == 'e').Count();
            double fE = mushromsTest.Except(resultTestE).Where(m => m.classValue != 'e').Count();

            var accuracyE = (vE + fE) / mushromsTest.Count();

            Console.WriteLine(string.Format("E Accuracy : {0}", accuracyE));

            double badClasified = 0;
            double wellClasified = 0;

            foreach (var mush in mushromsTest)
            {
                char classResult;
                var pFlags = pRules.Count(pr => pr.Rule.IsEqual(mush));
                var eFlags = eRules.Count(er => er.Rule.IsEqual(mush));
                if (pFlags >= eFlags)
                {
                    classResult = 'p';
                }
                else 
                {
                    classResult = 'e';
                }

                if (classResult == mush.classValue)
                {
                    wellClasified++;
                }
                else
                {
                    badClasified++;
                }
            }

            double totalAccuracy = wellClasified / (badClasified + wellClasified);

            Console.WriteLine(string.Format("TA Accuracy : {0}", totalAccuracy));

            Console.ReadKey();
        }

        private static IEnumerable<Mushroom> EvaluatePopulation(IEnumerable<Mushroom> elements, IEnumerable<MushroomRule> rules)
        {
            return elements.Where(e => rules.Any(r => e.IsEqual(r.Rule))).Distinct();
        }

        private static IEnumerable<Mushroom> ReadCsv()
        {
            var reader = new StreamReader(File.OpenRead("agaricus-lepiota.data"));

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine().Replace('?', '0');
                var values = line.Split(',');

                yield return new Mushroom(values);
            }
        }
    }
}
