using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CRUDApi.Models
{
    public class JobSearchRequest
    {
        [JsonProperty("keyword")]
        [JsonPropertyName("keyword")]
        [Required]
        public string? Keyword { get; set; }

        [JsonProperty("location")]
        [JsonPropertyName("location")]
        [Required]
        public string? Location { get; set; }

        public string? JsonResponse { get; set; }
    }
}
