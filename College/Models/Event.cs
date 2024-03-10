using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace College.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [AllowHtml]
        public string Description { get; set; }
    }
}