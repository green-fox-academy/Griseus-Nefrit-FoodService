namespace FoodService.Models
{
    public class Meal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int PriceAmount { get; set; }
        public string PriceCurrency { get; set; } = "ft";
    }
}
