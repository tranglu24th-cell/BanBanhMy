using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Database.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tin nhắn")]
        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
  
        public string? AdminReply { get; set; }

        public DateTime? RepliedAt { get; set; }
    }
}