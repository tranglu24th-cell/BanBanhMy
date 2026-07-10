using System.ComponentModel.DataAnnotations;

namespace Core.Database.Models
{
    /// <summary>
    /// Trạng thái đơn hàng. Lưu dưới dạng int trong DB (mặc định của EF Core cho enum).
    /// </summary>
    public enum OrderStatus
    {
        [Display(Name = "Chờ duyệt")]
        ChoDuyet = 0,

        [Display(Name = "Đang làm bánh")]
        DangLamBanh = 1,

        [Display(Name = "Đang giao")]
        DangGiao = 2,

        [Display(Name = "Đã hoàn thành")]
        DaHoanThanh = 3,

        [Display(Name = "Đã hủy")]
        DaHuy = 4
    }

    public static class OrderStatusExtensions
    {
        /// <summary>
        /// Những trạng thái được phép chuyển tới, tính từ trạng thái hiện tại.
        /// Giúp chặn việc chuyển trạng thái "nhảy cóc" hoặc đi ngược quy trình.
        /// </summary>
        public static bool CanTransitionTo(this OrderStatus current, OrderStatus target)
        {
            if (current == target) return false;

            return current switch
            {
                OrderStatus.ChoDuyet => target is OrderStatus.DangLamBanh or OrderStatus.DaHuy,
                OrderStatus.DangLamBanh => target is OrderStatus.DangGiao or OrderStatus.DaHuy,
                OrderStatus.DangGiao => target is OrderStatus.DaHoanThanh or OrderStatus.DaHuy,
                // Đã hoàn thành / Đã hủy là trạng thái đóng, không cho chuyển tiếp nữa
                OrderStatus.DaHoanThanh => false,
                OrderStatus.DaHuy => false,
                _ => false
            };
        }

        public static string GetDisplayName(this OrderStatus status)
        {
            return status switch
            {
                OrderStatus.ChoDuyet => "Chờ duyệt",
                OrderStatus.DangLamBanh => "Đang làm bánh",
                OrderStatus.DangGiao => "Đang giao",
                OrderStatus.DaHoanThanh => "Đã hoàn thành",
                OrderStatus.DaHuy => "Đã hủy",
                _ => status.ToString()
            };
        }
    }
}
