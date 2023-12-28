using AutoFixture;
using System.Text.Json.Serialization;

namespace DeserializationOptimizationCommon
{
    public class TestModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("stringProp1")]
        public string StringProp1 { get; set; }
        [JsonPropertyName("stringProp2")]
        public string StringProp2 { get; set; }
        [JsonPropertyName("stringProp3")]
        public string StringProp3 { get; set; }
        [JsonPropertyName("stringProp4")]
        public string StringProp4 { get; set; }
        [JsonPropertyName("stringProp5")]
        public string StringProp5 { get; set; }
        [JsonPropertyName("dateTimeProp1")]
        public DateTimeOffset DateTimeProp1 { get; set; }
        [JsonPropertyName("dateTimeProp2")]
        public DateTimeOffset DateTimeProp2 { get; set; }
        [JsonPropertyName("dateTimeProp3")]
        public DateTimeOffset DateTimeProp3 { get; set; }
        [JsonPropertyName("dateTimeProp4")]
        public DateTimeOffset DateTimeProp4 { get; set; }
        [JsonPropertyName("dateTimeProp5")]
        public DateTimeOffset DateTimeProp5 { get; set; }
        [JsonPropertyName("decimalProp1")]
        public decimal DecimalProp1 { get; set; }
        [JsonPropertyName("decimalProp2")]
        public decimal DecimalProp2 { get; set; }
        [JsonPropertyName("decimalProp3")]
        public decimal DecimalProp3 { get; set; }
        [JsonPropertyName("decimalProp4")]
        public decimal DecimalProp4 { get; set; }
        [JsonPropertyName("decimalProp5")]
        public decimal DecimalProp5 { get; set; }
        [JsonPropertyName("boolProp1")]
        public bool BoolProp1 { get; set; }
        [JsonPropertyName("boolProp2")]
        public bool BoolProp2 { get; set; }
        [JsonPropertyName("boolProp3")]
        public bool BoolProp3 { get; set; } = false;
        [JsonPropertyName("boolProp4")]
        public bool BoolProp4 { get; set; }
        [JsonPropertyName("boolProp5")]
        public bool BoolProp5 { get; set; }

        [JsonPropertyName("stringListProp1")]
        public List<string> StringListProp1 { get; set; }
        [JsonPropertyName("stringListProp2")]
        public List<string> StringListProp2 { get; set; }
        [JsonPropertyName("dateTimeOffsetListProp1")]
        public List<DateTimeOffset> DateTimeOffsetListProp1 { get; set; }
        [JsonPropertyName("dateTimeOffsetListProp2")]
        public List<DateTimeOffset?> DateTimeOffsetListProp2 { get; set; }
        [JsonPropertyName("decimalListProp1")]
        public List<decimal> DecimalListProp1 { get; set; }
        [JsonPropertyName("decimalListProp2")]
        public List<decimal> DecimalListProp2 { get; set; }

        public static TestModel GetModel()
        {
            var fixture = new Fixture();
            return fixture.Create<TestModel>();
        }
    }


}

