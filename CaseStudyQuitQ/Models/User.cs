using System;
using System.Collections.Generic;

namespace CaseStudyQuitQ.Models
{
    public partial class User
    {
        public User()
        {
            Carts = new HashSet<Cart>();
            DeliveryAddresses = new HashSet<DeliveryAddress>();
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? ContactNumber { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Cart>? Carts { get; set; }
        public virtual ICollection<DeliveryAddress>? DeliveryAddresses { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
    }
    public record UserDTO(string FirstName, string LastName, string UserName, string ContactNumber, string Password, string Email, string Role);

}
