using System.ComponentModel.DataAnnotations;

namespace Education.Model
{
    public abstract class ETableBase : EntityBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }
    }
}
