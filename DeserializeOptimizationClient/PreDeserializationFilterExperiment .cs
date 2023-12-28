using Confluent.Kafka;
using DeserializationOptimizationCommon;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace DeserializeOptimizationClient
{
    public class PreSerializationFilterExperiment : IExperiment
    {
        public string Name => "Pre Deserialization Experiment";

        public string Group => "PreDeserialization" + Guid.NewGuid();

        public int FilterValue => _filterValue;

        public int _filterValue;
        public PreSerializationFilterExperiment(int filterMessage)
        {
            _filterValue = filterMessage;
        }

        /// <summary>
        /// /// This Experiment reads a preset number of messages on the Topic.  Messages are filtered pre deserialization, the work component is Debug.Writeline
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
                        var headerID = cr.Headers.Where(h => h.Key == "id").FirstOrDefault();

                        if (headerID == null)
                            throw new Exception("Null Header");

                        if (BitConverter.ToInt32(headerID.GetValueBytes(), 0) <= _filterValue)
                        {
                            var m = System.Text.Json.JsonSerializer.Deserialize<TestModel>(cr.Message.Value);
                            Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key,-10} value = {cr.Message.Value}");
                        }

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
