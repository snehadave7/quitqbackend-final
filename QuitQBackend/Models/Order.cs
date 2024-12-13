using System;
using System.Collections.Generic;

namespace QuitQBackend.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? OrderDate { get; set; }= DateTime.Now;
        public int? AddressId { get; set; }

        public virtual User? User { get; set; }
        public virtual DeliveryAddress? DeliveryAddress { get; set; }
        public virtual ICollection<OrderItem>? OrderItems { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
    }
    public record OrderDTO(int? UserId, int Quantity);
}
