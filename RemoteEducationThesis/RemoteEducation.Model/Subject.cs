using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model
{
    public class Subject : EntityBase
    {
        public string Name { get; set; }

        [ForeignKey("Professor")]
        public int ProfessorID { get; set; }

        public User Professor { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
