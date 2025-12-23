namespace EsiTp2.Domain.Entities
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool Ativo { get; set; }
    }
}
