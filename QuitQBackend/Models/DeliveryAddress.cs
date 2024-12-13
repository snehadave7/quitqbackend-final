using System;
using System.Collections.Generic;

namespace QuitQBackend.Models
{
    public partial class DeliveryAddress
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Pincode { get; set; }
        public string? Phone { get; set; }
        public string? Notes { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<Order>? Order { get; set; }

    }
    public record DeliveryAddressDTO(int? UserId, string? Address, string? City, string? Pincode, string? Phone, string? Notes);
}
