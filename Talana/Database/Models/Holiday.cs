﻿using System.ComponentModel.DataAnnotations;

namespace Talana.Database.Models
{
    public class Holiday
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
