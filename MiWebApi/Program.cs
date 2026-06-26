using EspacioJoke;

using System.Text.Json; 
using System.Collections.Generic;
using System.Text.Json.Serialization;

List<Joke> jokes = await GetJokes();

foreach(Joke j in jokes)
{
    Console.WriteLine($"{j.setup}");
    Console.WriteLine(j.punchline);
    Console.WriteLine("-------------------------------------");
}

var opciones = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNameCaseInsensitive = true
};

string jsonString = JsonSerializer.Serialize(jokes, opciones);
string ruta = "datos.json";
File.WriteAllText(ruta, jsonString);

static async Task<List<Joke>> GetJokes()
{
    var url = $"https://official-joke-api.appspot.com/jokes/programming/ten";
    try
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        
        List<Joke> jokes = JsonSerializer.Deserialize<List<Joke>>(responseBody);

        Console.WriteLine(jokes[0].setup);
        Console.ReadKey();
        Console.WriteLine(jokes[0].punchline);
        Console.ReadKey();
        return jokes;
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Problemas de acceso a la API");
        Console.WriteLine("Message: {0} ", e.Message);
        return null;
    }
} 

