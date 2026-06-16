using System.Text.Json; 
﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

// Muestre en pantalla el nombre y correo electrónico y Domicilio de los primeros cinco usuarios.
List<Usuario> usuarios = await GetUsuarios();
foreach(Usuario u in usuarios){
    Console.WriteLine($"Nombre: {u.name}- Correo: {u.email}- Domicilio: {u.adress.city}");
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
            
            /*
            for(int i = 0; i < 5; i++){
                Console.WriteLine($"Nombre: {usuarios[i].name}- Correo: {usuarios[i].email}- Domicilio: {usuarios[i].adress.city}");
            }*/
            return usuarios;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Problemas de acceso a la API");
            Console.WriteLine("Message :{0} ", e.Message);
            return null;
        }
}