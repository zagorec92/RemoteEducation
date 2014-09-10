using System.Collections.Generic;

namespace Education.Model
{
    public class Question : EntityBase
    {
        /// <summary>
        /// HTML content of the question.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Collection of correct answers.
        /// </summary>
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
