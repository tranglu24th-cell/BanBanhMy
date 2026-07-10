using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models 
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        public string? Phone { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public TimeSpan BookingTime { get; set; }

        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // --- Phần dành cho Admin phản hồi ---
        public string? AdminReply { get; set; } // Lời nhắn của Admin
        public string Status { get; set; } = "Chờ duyệt"; // Trạng thái: Chờ duyệt, Đã xác nhận, Từ chối
        public DateTime? RepliedAt { get; set; } // Ngày admin phản hồi
    }
}