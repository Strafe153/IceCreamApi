using System;

namespace IceCreamApi.Models
{
    public class IceCream
    {
        public Guid Id { get; set; }
        public string Flavour { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public int WeightInGrams { get; set; }
    }
}
