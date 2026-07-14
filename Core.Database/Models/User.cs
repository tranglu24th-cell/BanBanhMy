using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models.EF
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } 

        public string? Username { get; set; }
        public string? Password { get; set; }
        
    }
}