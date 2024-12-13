using System;
using System.Collections.Generic;

namespace QuitQBackend.Models
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
    public record CartItemDTO(int? CartId, int? ProductId, int? Quantity);
}
