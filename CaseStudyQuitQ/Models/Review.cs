using System;
using System.Collections.Generic;

namespace CaseStudyQuitQ.Models
{
    public partial class Review
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public decimal? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? ReviewDate { get; set; }

        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
    public record ReviewDTO(int UserId, int ProductId, decimal Rating, string Comment);

}
