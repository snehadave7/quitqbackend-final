using System;
using System.Collections.Generic;

namespace CaseStudyQuitQ.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<CartItem>? CartItems { get; set; }
    }
    public record CartDTO(int UserId);

}
