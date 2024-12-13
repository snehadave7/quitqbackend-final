using System;
using System.Collections.Generic;

namespace QuitQBackend.Models
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public string? OrderStatus { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
    public record OrderItemDTO(int OrderId, int ProductId, int Quantity, string OrderStatus);
}
