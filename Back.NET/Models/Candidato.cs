namespace Back.NET.Models
{
    public class Candidato
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }        
        public string Technologies { get; set; }
    }
}
