using System;
using System.Collections.Generic;

namespace CaseStudyQuitQ.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
            SubCategories = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<SubCategory>? SubCategories { get; set; }
    }
    public record ProductCategoryDTO(string name);

}
