using System.Text.Json.Serialization;

namespace Domain.DomainEntity
{
    public class RateDomainEntity
    {
        [JsonPropertyName("from")]
        public string? From { get; set; }
        [JsonPropertyName("to")]
        public string? To { get; set; }
        [JsonPropertyName("rate")]
        public decimal Rate { get; set; }
    }
}
