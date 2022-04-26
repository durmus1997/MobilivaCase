namespace MobilivaCase
{
    public class CreateOrderRequest
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerGSM { get; set; } = string.Empty;
        public List<ProductDetail>? ProductDetail { get; set; }
    }

    public class ProductDetail
    {
        public int ProductId { get; set; }
        public int UnitPrice { get; set; }
        public int Amount { get; set; }
    }
    
}
