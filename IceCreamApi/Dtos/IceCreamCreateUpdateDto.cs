using System.ComponentModel.DataAnnotations;

namespace IceCreamApi.Dtos
{
    public record IceCreamCreateUpdateDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Flavour { get; init; }

        public string Color { get; init; }

        [Range(1, 30)]
        public decimal Price { get; init; }

        public int WeightInGrams { get; init; } = 80;
    }
}
