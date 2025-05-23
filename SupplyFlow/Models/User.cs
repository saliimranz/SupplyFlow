using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SupplyFlow.Models
{
    public class User
    {
        [Key]
        public int User_ID { get; set; }

        [Required]
        [MaxLength(255)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        public string Password_Hash { get; set; }

        [MaxLength(200)]
        public string Full_Name { get; set; }

        [MaxLength(50)]
        public string Role { get; set; }

        public DateTime Created_At { get; set; } = DateTime.Now;
    }
}