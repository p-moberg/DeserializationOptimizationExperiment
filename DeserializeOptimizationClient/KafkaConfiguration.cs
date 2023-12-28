namespace DeserializeOptimizationClient
{
    public static class KafkaConfiguration
    {
        public static int NumberOfMessagetoRead => 1000000;
        public static string GetTopic() => "MyTestTopic";

        public static List<KeyValuePair<string, string>> GetKafkaconfiguartion(string groupId)
        {
            var configuration = new List<KeyValuePair<string, string>>();
            configuration.Add(new KeyValuePair<string, string>("bootstrap.servers", "127.0.0.1:9092"));
            configuration.Add(new KeyValuePair<string, string>("group.id", groupId));
            configuration.Add(new KeyValuePair<string, string>("auto.offset.reset", "earliest"));
            configuration.Add(new KeyValuePair<string, string>("enable.auto.commit", "true"));

            return configuration;
        }
    }



}
