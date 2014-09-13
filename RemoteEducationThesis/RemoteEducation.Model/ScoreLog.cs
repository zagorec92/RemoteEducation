using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model
{
    public class ScoreLog : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("User")]
        public int UserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalScore { get; set; }
    }
}
