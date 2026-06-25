using System.Text.Json; 
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EspacioUsuarios;

// Muestre en pantalla el nombre y correo electrónico y Domicilio de los primeros cinco usuarios.
List<Usuario> usuarios = await GetUsuarios();

foreach(Usuario u in usuarios.Take(5)){
    Console.WriteLine($"{u.id}- Nombre: {u.name}");
    Console.WriteLine($"Correo electronico: {u.email}");
    Console.WriteLine($"Domicilio: {u.address.street}, {u.address.suite}, {u.address.city} ");
    Console.WriteLine("-------------------------------------------------");
}

static async Task<List<Usuario>> GetUsuarios(){
    var url = "https://jsonplaceholder.typicode.com/users/";
    try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            
            List<Usuario> usuarios = JsonSerializer.Deserialize<List<Usuario>>(responseBody);

            return usuarios;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Problemas de acceso a la API");
            Console.WriteLine("Message: {0} ", e.Message);
            return null;
        }
}