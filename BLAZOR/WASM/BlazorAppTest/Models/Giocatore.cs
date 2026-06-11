using System.Text.Json.Serialization;

namespace BlazorAppTest.Models
{
    public class Giocatore
    {
        [JsonPropertyName("codice")]
        public int Codice { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}