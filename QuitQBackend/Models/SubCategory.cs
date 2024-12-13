using System;
using System.Collections.Generic;

namespace QuitQBackend.Models
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? CategoryId { get; set; }

        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }

    public record SubCategoryDTO(string Name, int CategoryId);
}
