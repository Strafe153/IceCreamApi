using System.ComponentModel.DataAnnotations;

namespace IceCreamApi.Dtos
{
    public class IceCreamCreateUpdateDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Flavour { get; set; }

        public string Color { get; set; }

        [Range(1, 30)]
        public decimal Price { get; set; }

        public int WeightInGrams { get; set; } = 80;
    }
}
