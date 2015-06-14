using System.ComponentModel.DataAnnotations.Schema;
namespace Education.Model
{
    public class Answer : EntityBase
    {
        /// <summary>
        /// Gets or sets the content of the answer.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Gets or sets the question ID.
        /// </summary>
        [ForeignKey("Question")]
        public int QuestionID { get; set; }

        /// <summary>
        /// Gets or sets the question.
        /// </summary>
        public Question Question { get; set; }
    }
}
