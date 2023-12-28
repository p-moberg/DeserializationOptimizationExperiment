using Confluent.Kafka;
using DeserializationOptimizationCommon;
using System.Diagnostics;

namespace DeserializeOptimizationClient
{
    public class PostSerializationFilterExperiment : IExperiment
    {
        public string Name => "Post Deserialization Experiment";

        public string Group => "PostDeserialization" + Guid.NewGuid();
        public int FilterValue => _filterValue;
        public int _filterValue;
        public PostSerializationFilterExperiment(int filterMessage)
        {
            _filterValue = filterMessage;
        }

        /// <summary>
        /// This Experiment reads a preset number of messages on the Topic.  Messages are filtered post deserialization, the work component is Debug.Writeline.
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
                        if (m.Id <= _filterValue)
                            Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
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
