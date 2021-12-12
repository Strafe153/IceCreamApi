using System;

namespace IceCreamApi.Dtos
{
    public record IceCreamReadDto
    {
        public Guid Id { get; private set; }
        public string Flavour { get; private set; }
        public string Color { get; private set; }
        public decimal Price { get; private set; }
        public int WeightInGrams { get; private set; }
    }
}
