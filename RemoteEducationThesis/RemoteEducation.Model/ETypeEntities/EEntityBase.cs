using Education.Model.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Education.Model.ETypeEntities
{
    public abstract class EEntityBase : IEntity
    {
        /// <summary>
        /// Entity ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date created.
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}
