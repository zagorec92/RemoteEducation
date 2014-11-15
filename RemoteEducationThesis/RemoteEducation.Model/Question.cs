using System.Collections.Generic;

namespace Education.Model
{
    public class Question : EntityBase
    {
        /// <summary>
        /// Gets or sets the HTML content of the question.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the collection of correct answers.
        /// </summary>
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
