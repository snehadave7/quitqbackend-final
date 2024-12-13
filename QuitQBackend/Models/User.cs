using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuitQBackend.Models
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

        public string? FullName { get; set; }

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

    public record UserDTO(string FullName, string ContactNumber, string Password, string Email, string Role);
}
