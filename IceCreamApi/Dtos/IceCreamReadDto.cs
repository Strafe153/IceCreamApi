using System;

namespace IceCreamApi.Dtos
{
    public record IceCreamReadDto
    {
        public Guid Id { get; init; }
        public string Flavour { get; init; }
        public string Color { get; init; }
        public decimal Price { get; init; }
        public int WeightInGrams { get; init; }
    }
}
