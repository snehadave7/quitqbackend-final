using System;
using System.Collections.Generic;

namespace QuitQBackend.Models
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? StockQuantity { get; set; }

        public virtual Product? Product { get; set; }
    }
}
