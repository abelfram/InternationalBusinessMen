using System.Text.Json.Serialization;

namespace Infrastructure.Data.DataEntity
{

    public class RateDataEntity
    {
            [JsonPropertyName("from")]
            public string? From { get; set; }
            [JsonPropertyName("to")]
            public string? To { get; set; }
            [JsonPropertyName("rate")]
            public string Rate { get; set; }
        
    }
}
