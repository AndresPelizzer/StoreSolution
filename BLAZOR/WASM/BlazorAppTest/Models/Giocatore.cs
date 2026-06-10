using System.Text.Json.Serialization;

namespace BlazorAppTest.Models;

public class Giocatore
{

    [JsonPropertyName("id")] 
    public string Codice { get; set; }



    public string name { get; set; }
    public string username { get; set; }
    public string email { get; set; }
}
