namespace Education.Model
{
    public class Answer : EntityBase
    {
        /// <summary>
        /// Content of the answer.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Score
        /// </summary>
        public int Score { get; set; }
    }
}
