using Online_Quiz_Platform.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Online_Quiz_Platform.Models.Entities
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; } = string.Empty;

        public List<Option> Options { get; set; } = new List<Option>();

        public Guid Correctoption { get; set; }

        // New FK to Quiz
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }
    }
}