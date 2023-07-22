using Newtonsoft.Json; // For Newtonsoft.Json attributes
using System.ComponentModel.DataAnnotations; // For data annotations
using System.Text.Json.Serialization; // For System.Text.Json attributes (optional)

namespace CRUDApi.Models
{
    public class Job
    {
        // Primary key for the Job entity
        [Key]
        public int Id { get; set; }

        // Properties representing the various attributes of a Job

        // The following attributes specify the property names when serialized to JSON
        // NullValueHandling.Ignore indicates that if the value is null, it should be ignored in JSON serialization.

        [JsonProperty("company_logo_url", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("company_logo_url")]
        public string? CompanyLogoUrl { get; set; }

        [JsonProperty("company_name", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }

        [JsonProperty("company_rating", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("company_rating")]
        public string? CompanyRating { get; set; }

        [JsonProperty("company_review_link", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("company_review_link")]
        public string? CompanyReviewLink { get; set; }

        [JsonProperty("company_reviews", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("company_reviews")]
        public int? CompanyReviews { get; set; }

        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonProperty("job_location", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("job_location")]
        public string? JobLocation { get; set; }

        // Required data annotation indicates that this property must have a value (cannot be null or empty)
        [Required]
        [JsonProperty("job_title")]
        [JsonPropertyName("job_title")]
        public string? JobTitle { get; set; }

        [JsonProperty("job_url", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("job_url")]
        public string? JobUrl { get; set; }

        [JsonProperty("multiple_hiring", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("multiple_hiring")]
        public string? MultipleHiring { get; set; }

        [JsonProperty("next_page", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("next_page")]
        public string? NextPage { get; set; }

        [JsonProperty("page_number", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("page_number")]
        public int? PageNumber { get; set; }

        [JsonProperty("position", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonProperty("salary", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("salary")]
        public string? Salary { get; set; }

        [JsonProperty("urgently_hiring", NullValueHandling = NullValueHandling.Ignore)]
        [JsonPropertyName("urgently_hiring")]
        public string? UrgentlyHiring { get; set; }
    }
}
