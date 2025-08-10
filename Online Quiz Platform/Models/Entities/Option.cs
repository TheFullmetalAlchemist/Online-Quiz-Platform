using System.ComponentModel.DataAnnotations;

namespace Online_Quiz_Platform.Models.Entities
{
    public class Option
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string? Text { get; set; } 

        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
