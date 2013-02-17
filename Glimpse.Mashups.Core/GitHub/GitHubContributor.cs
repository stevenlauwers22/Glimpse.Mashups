using Newtonsoft.Json;

namespace Glimpse.Mashups.Core.GitHub
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GitHubContributor
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "login")]
        public string Login { get; set; }

        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty(PropertyName = "contributions")]
        public int Contributions { get; set; }
    }
}