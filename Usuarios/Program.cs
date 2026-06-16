using System.Text.Json; 
// Muestre en pantalla el nombre y correo electrónico y Domicilio de los primeros cinco usuarios.
await GetUsuarios();

static async Task GetUsuarios(){
    var url = "https://jsonplaceholder.typicode.com/users/";
    try
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            List<Usuario> usuarios = JsonSerializer.Deserialize<List<Usuario>>(responseBody);
            for(int i = 0; i < 5; i++){
                Console.WriteLine($"Nombre: {usuarios[i].Nombre}- Correo: {usuarios[i].Correo}- Domicilio: {usuarios[i].Domicilio}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Problemas de acceso a la API");
            Console.WriteLine("Message :{0} ", e.Message);
        }
}