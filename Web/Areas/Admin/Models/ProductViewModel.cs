namespace Web.Areas.Admin.Models
{
    public class ProductViewModel
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Intro { get; set; }
        public string? Content { get; set; }
        public double Price { get; set; } // Kiểu dữ liệu gốc đang là double
        public string? Picture { get; set; }
        public Guid? CategoryId { get; set; }
        public bool IsSale { get; set; }

        // ĐỔI TỪ decimal? THÀNH double? TẠI ĐÂY
        public double? PriceSale { get; set; }
        public int StockQuantity { get; set; }
    }
}