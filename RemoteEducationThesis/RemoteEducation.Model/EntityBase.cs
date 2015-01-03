using System;
using System.ComponentModel.DataAnnotations;

namespace Education.Model
{
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the entity ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        public DateTime? DateModified { get; set; }
    }
}
