﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace aspnet6_app.Models.Domain
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Name { get; set; }
        public int Variant { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
