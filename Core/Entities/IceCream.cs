namespace Core.Entities
{
    public class IceCream
    {
        public string Id { get; set; } = $"ice-cream:{Guid.NewGuid()}";
        public string? Flavour { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public int WeightInGrams { get; set; }
    }
}
