using System;
using System.Collections.Generic;

namespace CaseStudyQuitQ.Models
{
    public partial class Order
    {
        public Order()
        {
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? Quantity { get; set; }
        public string? OrderStatus { get; set; }
        public int? AddressId { get; set; } // New column
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
        public virtual DeliveryAddress? Address { get; set; }

        public virtual ICollection<Payment>? Payments { get; set; }
    }
    public record OrderDTO(int? UserId, int ProductId, DateTime OrderDate, int Quantity, string OrderStatus);

}
