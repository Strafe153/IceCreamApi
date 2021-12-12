using System;
using System.ComponentModel.DataAnnotations;

namespace IceCreamApi.Models
{
    public class IceCream
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Flavour { get; set; }

        public string Color { get; set; }

        [Required]
        [Range(1, 30)]
        public decimal Price { get; set; }

        public int WeightInGrams { get; set; } = 80;
    }
}
