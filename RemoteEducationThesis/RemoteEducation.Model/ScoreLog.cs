using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model
{
    public class ScoreLog : EntityBase
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [ForeignKey("User")]
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the total score.
        /// </summary>
        public decimal TotalScore { get; set; }
    }
}
