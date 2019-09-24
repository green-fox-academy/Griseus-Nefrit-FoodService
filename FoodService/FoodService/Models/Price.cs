namespace FoodService.Models
{
    public class Price
    {
        public long PriceId { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; } = "Ft";
    }
}