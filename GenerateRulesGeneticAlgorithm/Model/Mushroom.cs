using GeneticAlgorithm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateRulesGeneticAlgorithm.Model
{
    public class Mushroom
    {
        public char classValue { get; set; }
        public char capShape { get; set; }
        public char capSurface { get; set; }
        public char capColor { get; set; }
        public char bruises { get; set; }
        public char odor { get; set; }
        public char gillAttachment { get; set; }
        public char gillSpacing { get; set; }
        public char gillSize { get; set; }
        public char gillColor { get; set; }
        public char stalkShape { get; set; }
        public char stalkRoot { get; set; }
        public char stalkSurfaceAboveRing { get; set; }
        public char stalkSurfaceBelowRing { get; set; }
        public char stalkColorAboveRing { get; set; }
        public char stalkColorBelowRing { get; set; }
        public char veilType { get; set; }
        public char veilColor { get; set; }
        public char ringNumber { get; set; }
        public char ringType { get; set; }
        public char sporePrintColor { get; set; }
        public char population { get; set; }
        public char habitat { get; set; }

        public Mushroom()
        {
        }

        public Mushroom(IEnumerable<string> values)
        {
            classValue = values.ElementAt(0)[0];
            capShape = values.ElementAt(1)[0];
            capSurface = values.ElementAt(2)[0];
            capColor = values.ElementAt(3)[0];
            bruises = values.ElementAt(4)[0];
            odor = values.ElementAt(5)[0];
            gillAttachment = values.ElementAt(6)[0];
            gillSpacing = values.ElementAt(7)[0];
            gillSize = values.ElementAt(8)[0];
            gillColor = values.ElementAt(9)[0];
            stalkShape = values.ElementAt(10)[0];
            stalkRoot = values.ElementAt(11)[0];
            stalkSurfaceAboveRing = values.ElementAt(12)[0];
            stalkSurfaceBelowRing = values.ElementAt(13)[0];
            stalkColorAboveRing = values.ElementAt(14)[0];
            stalkColorBelowRing = values.ElementAt(15)[0];
            veilType = values.ElementAt(16)[0];
            veilColor = values.ElementAt(17)[0];
            ringNumber = values.ElementAt(18)[0];
            ringType = values.ElementAt(19)[0];
            sporePrintColor = values.ElementAt(20)[0];
            population = values.ElementAt(21)[0];
            habitat = values.ElementAt(22)[0];
        }

        public bool IsEqual(Mushroom another)
        {
            if (another.classValue == 0 || another.classValue == classValue) return true;
            if (another.capShape == 0 || another.capShape == capShape) return true;
            if (another.capSurface == 0 || another.capSurface == capSurface) return true;
            if (another.capColor == 0 || another.capColor == capColor) return true;
            if (another.bruises == 0 || another.bruises == bruises) return true;
            if (another.odor == 0 || another.odor == odor) return true;
            if (another.gillAttachment == 0 || another.gillAttachment == gillAttachment) return true;
            if (another.gillSpacing == 0 || another.gillSpacing == gillSpacing) return true;
            if (another.gillSize == 0 || another.gillSize == gillSize) return true;
            if (another.gillColor == 0 || another.gillColor == gillColor) return true;
            if (another.stalkShape == 0 || another.stalkShape == stalkShape) return true;
            if (another.stalkRoot == 0 || another.stalkRoot == stalkRoot) return true;
            if (another.stalkSurfaceAboveRing == 0 || another.stalkSurfaceAboveRing == stalkSurfaceAboveRing) return true;
            if (another.stalkSurfaceBelowRing == 0 || another.stalkSurfaceBelowRing == stalkSurfaceBelowRing) return true;
            if (another.stalkColorAboveRing == 0 || another.stalkColorAboveRing == stalkColorAboveRing) return true;
            if (another.stalkColorBelowRing == 0 || another.stalkColorBelowRing == stalkColorBelowRing) return true;
            if (another.veilType == 0 || another.veilType == veilType) return true;
            if (another.veilColor == 0 || another.veilColor == veilColor) return true;
            if (another.ringNumber == 0 || another.ringNumber == ringNumber) return true;
            if (another.ringType == 0 || another.ringType == ringType) return true;
            if (another.sporePrintColor == 0 || another.sporePrintColor == sporePrintColor) return true;
            if (another.population == 0 || another.population == population) return true;
            if (another.habitat == 0 || another.habitat == habitat) return true;
            return false;
        }
    }
}
