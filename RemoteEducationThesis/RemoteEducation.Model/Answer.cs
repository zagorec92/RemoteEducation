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
    }
}
