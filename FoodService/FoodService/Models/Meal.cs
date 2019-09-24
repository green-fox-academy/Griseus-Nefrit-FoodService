namespace FoodService.Models
{
    public class Meal
    {
        public long MealId { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public int PriceAmount { get; set; }
        public string PriceCurrency { get; set; } = "ft";
        
        public long RestaurantId { get; set; }
    }
}
