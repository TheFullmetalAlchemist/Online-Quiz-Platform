using System;

namespace Online_Quiz_Platform.Models.Entities
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        public string UserEmail { get; set; } = string.Empty;

        public int Score { get; set; }
        public DateTime AttemptDate { get; set; } = DateTime.Now;

        // If you want to tie it to a Quiz
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }
    }
}
