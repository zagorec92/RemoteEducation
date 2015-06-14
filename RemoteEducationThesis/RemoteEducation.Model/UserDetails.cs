using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.Model
{
    public class UserDetails : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public byte[] Picture { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets the first and last name combined.
        /// </summary>
        [NotMapped]
        public string FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the age.
        /// </summary>
        [Column(TypeName = "Date")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets the age.
        /// </summary>
        [NotMapped]
        public int Age
        {
            get { return DateTime.Now.Year - DateOfBirth.Year; }
        }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [MaxLength(1, ErrorMessage = "Gender value cannot have more than one character,")]
        [Column(TypeName = "char")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the collection of subjects. [if role = professor]
        /// </summary>
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
