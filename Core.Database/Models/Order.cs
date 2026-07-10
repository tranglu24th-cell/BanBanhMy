using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Database.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [ForeignKey("CustomerId")]
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        /// <summary>
        /// Trạng thái đơn hàng: Chờ duyệt -> Đang làm bánh -> Đang giao -> Đã hoàn thành (hoặc Đã hủy).
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.ChoDuyet;

        public ICollection<Details>Details { get; set; } = new HashSet<Details>();
    }
}
