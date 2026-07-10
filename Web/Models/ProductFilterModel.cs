namespace Web.Models
{
    public class ProductFilterModel
    {
        public Guid? CategoryId { get; set; }
        public string? PriceRange { get; set; } // "under50", "50to100", "over100"
        public string? SortBy { get; set; } // "price_asc", "price_desc", "newest"
        public int Page { get; set; } = 1; // Trang hiện tại
        public int PageSize { get; set; } = 6; // Số sản phẩm trên mỗi trang chủ
    }
}