﻿using System;
using System.Collections.Generic;

namespace CaseStudyQuitQ.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public string? Status { get; set; }
        public string? Method { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Order? Order { get; set; }
    }
    public record PaymentDTO(int? OrderId, string? Status, string? Method);

}
