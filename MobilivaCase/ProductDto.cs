namespace MobilivaCase
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
    }
}