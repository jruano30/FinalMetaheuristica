﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Interfaces
{
    public interface IMotherNature
    {
        IEnumerable<IBe> Evolve(Action<IEnumerable<IBe>, int> perIteractionAction);
    }
}
