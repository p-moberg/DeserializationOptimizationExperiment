using DeserializationOptimizationCommon;
using Confluent.Kafka;
using System.Diagnostics;

const string topic = "MyTestTopic";
var configuration = new List<KeyValuePair<string, string>>();
configuration.Add(new KeyValuePair<string, string>("bootstrap.servers", "127.0.0.1:9092"));
using (var producer = new ProducerBuilder<int, string>(
    configuration.AsEnumerable()).Build())
{
    var numProduced = 0;
    var st = new Stopwatch();
    st.Start();
    for (int i = 1; i <= 10000; i++)
    {
        for (int j = 1; j <= 100; j++)
        {
            var m = TestModel.GetModel();
            m.Id = j;
            var m_string = System.Text.Json.JsonSerializer.Serialize(m);
            var headers = new Headers();
            headers.Add("id", BitConverter.GetBytes(j));
            producer.Produce(topic, new Message<int, string> { Key = j, Value = m_string, Headers = headers },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError)
                        {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else
                        {
                            Console.WriteLine($"Produced event to topic {topic}: key = {j,-10} value = {m.Id}");
                            numProduced += 1;
                        }
                    });
        }
    }
    st.Stop();
    Console.WriteLine(st.ElapsedMilliseconds);
    Console.ReadLine();
}