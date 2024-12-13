using System;
using System.Collections.Generic;

namespace QuitQBackend.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            Inventories = new HashSet<Inventory>();
            OrderItems = new HashSet<OrderItem>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? ImageUrl { get; set; }
        public int? SellerId { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }

        public virtual ProductCategory? Category { get; set; }
        public virtual User? Seller { get; set; }
        public virtual SubCategory? SubCategory { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
        public virtual ICollection<Inventory>? Inventories { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
    public record ProductDTO(string? Name, string? Description, decimal? Price, int? Stock, string? ImageUrl, int? SellerId, int? CategoryId, int? SubCategoryId);
    public record ProductDTOForUser(string? Name, string? Description, decimal? Price, string? ImageUrl);
}
