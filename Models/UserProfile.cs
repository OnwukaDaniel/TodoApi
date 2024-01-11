using System.Text.Json.Serialization;

namespace Management.Models
{
    public class UserProfile
    {
        [JsonIgnore]
        public int id { get; set; }

        [JsonIgnore]
        public String? Uid { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
