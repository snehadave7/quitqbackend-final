using System;
using System.Collections.Generic;

namespace CaseStudyQuitQ.Models
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

        public virtual ProductCategory? Category { get; set; } // navigation property references parent ProdcutCategory
        public virtual ICollection<Product>? Products { get; set; } // one subcategory can have many products
    }
    public record SubCategoryDTO(string Name, int CategoryId);

}
