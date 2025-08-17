using System;
using System.Collections.Generic;

namespace Online_Quiz_Platform.Models.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public List<Question> Questions { get; set; } = new List<Question>();
        public List<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
    }
}
