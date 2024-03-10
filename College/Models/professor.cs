using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace College.Models
{
    public class Professor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public Department Department { get; set; }

        [Required]
        [Display(Name = "Department")]
        public byte DepartmentId { get; set; }

        [Display(Name = "Experience In Years")]
        public byte ExperienceInYears { get; set; }

        public Post Post { get; set; }

        [Required]
        [Display(Name = "Post")]
        public byte PostId { get; set; }
    }
}