using System.Text.Json.Serialization;

namespace SmileBoyClient.Core.Models
{
    /// <summary>
    /// Token entities
    /// </summary>
    class JwtResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public string IssusedTokenType { get; set; }
    }
}
