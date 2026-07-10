using System;

namespace Web.Models
{
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public string Title { get; set; } = "";
        public string? Picture { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double Total => Price * Amount;
    }
}