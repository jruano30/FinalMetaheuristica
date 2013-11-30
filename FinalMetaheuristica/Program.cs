﻿using GenerateRulesGeneticAlgorithm.Model;
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

namespace FinalMetaheuristica
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Mushroom> mushromsAll = ReadCsv().OrderBy(m => new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0)).NextDouble()).ToList();

            var trainigPercentage = 0.8;

            var mushroms = mushromsAll.Take(((int)Math.Round(mushromsAll.Count() * trainigPercentage, 0)));

            var mushromsTest = mushromsAll.Take(((int)Math.Round(mushromsAll.Count() * (1 - trainigPercentage), 0)));

            IEnumerable<IEnumerable<char>> posibleValues = GetPosibleValues<Mushroom, char>(mushroms).ToList();

            var classValue = 'p';

            IEnumerable<IMutator> mutators = new List<IMutator>()
            {
                new GeneralizeRuleMutator()
                //new EspecifyRuleMutator(posibleValues)
            };

            IEnumerable<IConceiver> conceivers = new List<IConceiver>() 
            {
                new FatherLeftMotherRightConceiver(),
                new MotherLeftFatherRightConceiver()
            };

            var initialRules = GetRandomMushroomRules(mushroms, 1000, classValue).Select(m => new MushroomRule(new RulesProvider(mushroms), 0, classValue) { Rule = m }).ToList();

            var motherNature = new MotherNature(
                initialRules,
                new DarwinRules(mutators, conceivers),
                new God() { maximunIteration = 100, maximunOffspring = 500 },
                new RandomOperations(),
                new MushroomRuleComparer());

            Console.WriteLine("Initial Rules");

            foreach (MushroomRule rule in initialRules)
            {
                Console.WriteLine(rule.Rule.ToString() + " = " + rule.Solution);
            }

            var rules = motherNature.Evolve((rulesP, iteration) =>
            {
                var best = rulesP.FirstOrDefault();
                Console.Write(string.Format("\r{0}/{1} S = {2}", best.Generation, iteration, best.Solution));
            });

            Console.WriteLine("Final Rules");

            var mRules = rules.Select((r => r as MushroomRule));

            foreach (var rule in mRules.OrderBy(r => r.Rule.ToString()))
            {
                Console.WriteLine(rule.Rule.ToString() + " = " + rule.Solution);
            }

            IEnumerable<Mushroom> result = EvaluatePopulation(mushroms, mRules);
            IEnumerable<Mushroom> resultTest = EvaluatePopulation(mushromsTest, mRules);

            Console.WriteLine(string.Format("{0}/{1} {2}-{3}", result.Where(m => m.classValue == classValue).Count(), result.Count(), mushroms.Where(m => m.classValue == classValue).Count(), mushroms.Count()));

            Console.WriteLine(string.Format("{0}/{1} {2}-{3}", resultTest.Where(m => m.classValue == classValue).Count(), resultTest.Count(), mushromsTest.Where(m => m.classValue == classValue).Count(), mushromsTest.Count()));

            Console.ReadKey();
        }

        private static IEnumerable<Mushroom> EvaluatePopulation(IEnumerable<Mushroom> elements, IEnumerable<MushroomRule> rules)
        {
            return elements.Where(e => rules.Any(r => e.IsEqual(r.Rule))).Distinct();
        }

        private static IEnumerable<IEnumerable<R>> GetPosibleValues<T, R>(IEnumerable<T> items)
        {
            var first = new List<IEnumerable<R>>();

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            for (var i = 0; i < props.Length; i++)
            {
                var values = new List<R>();
                foreach (var item in items)
                {
                    values.Add(((R)props[i].GetValue(item, null)));
                }

                yield return values.Distinct().ToList();
            }
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

        private static IEnumerable<Mushroom> GetRandomMushroomRules(IEnumerable<Mushroom> mushrooms, int numberOfRules, char classValue)
        {
            var zerosCount = 10;

            var capShape = mushrooms.Where(m => m.classValue == classValue).Select(m => m.capShape).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var capSurface = mushrooms.Where(m => m.classValue == classValue).Select(m => m.capSurface).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var capColor = mushrooms.Where(m => m.classValue == classValue).Select(m => m.capColor).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var bruises = mushrooms.Where(m => m.classValue == classValue).Select(m => m.bruises).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var odor = mushrooms.Where(m => m.classValue == classValue).Select(m => m.odor).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var gillAttachment = mushrooms.Where(m => m.classValue == classValue).Select(m => m.gillAttachment).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var gillSpacing = mushrooms.Where(m => m.classValue == classValue).Select(m => m.gillSpacing).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var gillSize = mushrooms.Where(m => m.classValue == classValue).Select(m => m.gillSize).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var gillColor = mushrooms.Where(m => m.classValue == classValue).Select(m => m.gillColor).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var stalkShape = mushrooms.Where(m => m.classValue == classValue).Select(m => m.stalkShape).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var stalkRoot = mushrooms.Where(m => m.classValue == classValue).Select(m => m.stalkRoot).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var stalkSurfaceAboveRing = mushrooms.Where(m => m.classValue == classValue).Select(m => m.stalkSurfaceAboveRing).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var stalkSurfaceBelowRing = mushrooms.Where(m => m.classValue == classValue).Select(m => m.stalkSurfaceBelowRing).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var stalkColorAboveRing = mushrooms.Where(m => m.classValue == classValue).Select(m => m.stalkColorAboveRing).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var stalkColorBelowRing = mushrooms.Where(m => m.classValue == classValue).Select(m => m.stalkColorBelowRing).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var veilType = mushrooms.Where(m => m.classValue == classValue).Select(m => m.veilType).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var veilColor = mushrooms.Where(m => m.classValue == classValue).Select(m => m.veilColor).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var ringNumber = mushrooms.Where(m => m.classValue == classValue).Select(m => m.ringNumber).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var ringType = mushrooms.Where(m => m.classValue == classValue).Select(m => m.ringType).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var sporePrintColor = mushrooms.Where(m => m.classValue == classValue).Select(m => m.sporePrintColor).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var population = mushrooms.Where(m => m.classValue == classValue).Select(m => m.population).Distinct().Concat(Enumerable.Repeat('0', zerosCount));
            var habitat = mushrooms.Where(m => m.classValue == classValue).Select(m => m.habitat).Distinct().Concat(Enumerable.Repeat('0', zerosCount));

            var random = new Random();

            for (int i = 0; i < numberOfRules; i++)
            {
                yield return new Mushroom
                {
                    classValue = '0',
                    capShape = capShape.ElementAt(random.Next(0, capShape.Count())),
                    capSurface = capSurface.ElementAt(random.Next(0, capSurface.Count())),
                    capColor = capColor.ElementAt(random.Next(0, capColor.Count())),
                    bruises = bruises.ElementAt(random.Next(0, bruises.Count())),
                    odor = odor.ElementAt(random.Next(0, odor.Count())),
                    gillAttachment = gillAttachment.ElementAt(random.Next(0, gillAttachment.Count())),
                    gillSpacing = gillSpacing.ElementAt(random.Next(0, gillSpacing.Count())),
                    gillSize = gillSize.ElementAt(random.Next(0, gillSize.Count())),
                    gillColor = gillColor.ElementAt(random.Next(0, gillColor.Count())),
                    stalkShape = stalkShape.ElementAt(random.Next(0, stalkShape.Count())),
                    stalkRoot = stalkRoot.ElementAt(random.Next(0, stalkRoot.Count())),
                    stalkSurfaceAboveRing = stalkSurfaceAboveRing.ElementAt(random.Next(0, stalkSurfaceAboveRing.Count())),
                    stalkSurfaceBelowRing = stalkSurfaceBelowRing.ElementAt(random.Next(0, stalkSurfaceBelowRing.Count())),
                    stalkColorAboveRing = stalkColorAboveRing.ElementAt(random.Next(0, stalkColorAboveRing.Count())),
                    stalkColorBelowRing = stalkColorBelowRing.ElementAt(random.Next(0, stalkColorBelowRing.Count())),
                    veilType = veilType.ElementAt(random.Next(0, veilType.Count())),
                    veilColor = veilColor.ElementAt(random.Next(0, veilColor.Count())),
                    ringNumber = ringNumber.ElementAt(random.Next(0, ringNumber.Count())),
                    ringType = ringType.ElementAt(random.Next(0, ringType.Count())),
                    sporePrintColor = sporePrintColor.ElementAt(random.Next(0, sporePrintColor.Count())),
                    population = population.ElementAt(random.Next(0, population.Count())),
                    habitat = habitat.ElementAt(random.Next(0, habitat.Count()))
                };
            }
        }
    }
}
