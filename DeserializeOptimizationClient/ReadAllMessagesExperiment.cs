using Confluent.Kafka;
using DeserializationOptimizationCommon;
using System.Diagnostics;

namespace DeserializeOptimizationClient
{
    public class ReadAllMessagesExperiment : IExperiment
    {
        public string Name => "Read All Messages No Filtering";

        public string Group => "UnFilteredRead" + Guid.NewGuid();

        public int FilterValue => 0;

        /// <summary>
        /// This Experiment reads a preset number of messages on the Topic.  Messages are not filtered, there is no 'work component' to the experiment.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public long ReadMessages(string group)
        {
            var st = new Stopwatch();
            st.Start();
            var topic = KafkaConfiguration.GetTopic();
            var configuration = KafkaConfiguration.GetKafkaconfiguartion(group);
            using (var consumer = new ConsumerBuilder<string, string>(
                        configuration.AsEnumerable()).Build())
            {
                consumer.Subscribe(topic);
                try
                {
                    var i = 1;
                    var NumberOfMessages = KafkaConfiguration.NumberOfMessagetoRead;
                    while (i < NumberOfMessages)
                    {
                        var cr = consumer.Consume();
                        i++;
                        var m = System.Text.Json.JsonSerializer.Deserialize<TestModel>(cr.Message.Value);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
                finally
                {
                    st.Stop();
                    consumer?.Close();
                    consumer?.Dispose();
                }
            }
            return st.ElapsedMilliseconds;
        }
    }
}
