using System.Runtime.CompilerServices;
﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
﻿using System.Net.WebSockets;
using System.Text.Json;


List<Tarea> tareas = await GetTareas();

// mostrar primero las tareas pendientes
foreach(Tarea tarea in tareas){
    if(!tarea.Completed){
        Console.WriteLine($"Titulo: {tarea.Title}- Estado: Pendiente");
    }
}
foreach(Tarea tarea in tareas){
    if(tarea.Completed){
        Console.WriteLine($"Titulo: {tarea.Title}- Estado: Completada");
    }
}

// serializar y guardar en tareas.json
var options = new JsonSerializerOptions {WriteIndented = true};
string json = JsonSerializer.Serialize(tareas, options);
string archivo = "./tareas.json";
File.WriteAllText(archivo, json);

static async Task<List<Tarea>> GetTareas(){
    var url = "https://jsonplaceholder.typicode.com/todos/";
    try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Tarea> tareas = JsonSerializer.Deserialize<List<Tarea>>(responseBody);
            return tareas;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Problemas de acceso a la API");
            Console.WriteLine("Message :{0} ", e.Message);
            return null;
        }
}

public class Tarea{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("id")]
    public int ID { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("completed")]
    public bool Completed { get; set; }
}


