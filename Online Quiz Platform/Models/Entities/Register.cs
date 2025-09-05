namespace Online_Quiz_Platform.Models.Entities
{
    public class Register
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? ConfirmPassword { get; set; } 
    }
}
