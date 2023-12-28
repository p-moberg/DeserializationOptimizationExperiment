using DeserializeOptimizationClient;
using System.Diagnostics;
List<string> results = new List<string>();

RunExperiment(new ReadAllMessagesExperiment());
RunExperiment(new PreSerializationFilterExperiment(0)); // 0% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(1)); // 1% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(5)); // 5% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(10)); // 10% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(20)); // 20% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(30)); // 30% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(40)); // 40% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(50)); // 50% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(60)); // 60% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(70)); // 70% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(80)); // 80% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(90)); // 90% are valid for processing
RunExperiment(new PreSerializationFilterExperiment(100)); // 100% are valid for processing
RunExperiment(new PostSerializationFilterExperiment(0));
RunExperiment(new PostSerializationFilterExperiment(1));
RunExperiment(new PostSerializationFilterExperiment(5));
RunExperiment(new PostSerializationFilterExperiment(10));
RunExperiment(new PostSerializationFilterExperiment(20));
RunExperiment(new PostSerializationFilterExperiment(30));
RunExperiment(new PostSerializationFilterExperiment(40));
RunExperiment(new PostSerializationFilterExperiment(50));
RunExperiment(new PostSerializationFilterExperiment(60));
RunExperiment(new PostSerializationFilterExperiment(70));
RunExperiment(new PostSerializationFilterExperiment(80));
RunExperiment(new PostSerializationFilterExperiment(90));
RunExperiment(new PostSerializationFilterExperiment(100));

using (StreamWriter outputFile = new StreamWriter(Path.Combine("c:\\temp\\", "WriteLines.txt")))
{
    foreach (var result in results)
    {
        outputFile.WriteLine(result);
    }
}
void RunExperiment(IExperiment experiment)
{
    var numberofRepititions = 3;

    for (int i = 1; i <= numberofRepititions; i++)
    {
        var exp = $"{experiment.Name},{experiment.FilterValue},{experiment.ReadMessages(experiment.Group + "-" + i.ToString())}";
        results.Add(exp);
        Debug.WriteLine(exp);
    }
}

