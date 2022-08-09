namespace Core.Entities
{
    public class IceCream
    {
        public int Id { get; set; }
        public string? Flavour { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public int WeightInGrams { get; set; }
    }
}
