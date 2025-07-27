namespace Online_Quiz_Platform.Models.Entities
{
    public class Login
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        
    }
}
