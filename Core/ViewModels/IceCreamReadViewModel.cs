namespace Core.ViewModels
{
    public record IceCreamReadViewModel
    {
        public string? Id { get; init; }
        public string? Flavour { get; init; }
        public string? Color { get; init; }
        public decimal Price { get; init; }
        public int WeightInGrams { get; init; }
    }
}
