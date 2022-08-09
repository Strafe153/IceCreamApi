namespace Core.ViewModels
{
    public record IceCreamCreateUpdateViewModel
    {
        public string? Flavour { get; init; }
        public string? Color { get; init; }
        public decimal Price { get; init; }
        public int WeightInGrams { get; init; }
    }
}
