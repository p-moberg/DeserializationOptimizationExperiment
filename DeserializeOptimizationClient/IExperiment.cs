using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeserializeOptimizationClient
{
    public interface IExperiment
    {
        string Name { get; }
        string Group { get; }
        int FilterValue { get; }
        long ReadMessages(string group);
    }
}
