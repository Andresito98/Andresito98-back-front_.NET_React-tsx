using System.Text.Json.Serialization;

namespace Back.NET.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore] public string Password { get; set; }
        // Este json lo que hace es que oculta la contraseña al hacer una peticion, aunque este encriptada
    }

}
