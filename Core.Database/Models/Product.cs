using Core.Database.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Database.Models
{
    [Table("Product")]
    public class Product: IAuditable
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; } = "";
        [MaxLength(50)]
        public string? Picture { get; set; }
        [MaxLength(500)]
        public string? Intro { get; set; }
        public string? Content { get; set; }
        public double Price { get; set; }
        public double? PriceSale { get; set; } // Giá sau khi giảm (để kiểu nullable phòng trường hợp không giảm giá)
        public bool IsSale { get; set; }

        /// <summary>
        /// Số lượng tồn kho. Bị trừ khi đơn hàng được duyệt sang trạng thái "Đang làm bánh".
        /// </summary>
        public int StockQuantity { get; set; } = 0;
        public bool? IsComming { get; set; }
        [ForeignKey("CategoryId")]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public ICollection<Details> Details { get; set; } = new HashSet<Details>();
    }
}
