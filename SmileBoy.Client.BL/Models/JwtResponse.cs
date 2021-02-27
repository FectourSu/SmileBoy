using SmileBoy.Client.Core.Helpers;
using System.Text.Json.Serialization;

namespace SmileBoyClient.Core.Models
{
    /// <summary>
    /// Token entities
    /// </summary>
    public class JwtResponse
    {
        [JsonPropertyName(Token.Access)]
        public string AccessToken { get; set; }

        [JsonPropertyName(Token.Refresh)]
        public string RefreshToken { get; set; }

        [JsonPropertyName(Token.Type)]
        public string IssusedTokenType { get; set; }
    }
}
