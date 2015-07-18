using Education.Model.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model.Entities
{
	public abstract class EntityBase : IEntity
    {
        /// <summary>
        /// Gets or sets the entity ID.
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        public DateTime? DateModified { get; set; }
    }
}
