using System;
using System.ComponentModel.DataAnnotations;

namespace RemoteEducation.Model
{
    public abstract class EntityBase
    {
        /// <summary>
        /// Entity ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// DateCreated.
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// DateModified.
        /// </summary>
        public DateTime? DateModified { get; set; }
    }
}
