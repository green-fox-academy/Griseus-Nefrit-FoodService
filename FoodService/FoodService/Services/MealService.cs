namespace FoodService.Services
{
    public class MealService : IMealService
    {
        private readonly ApplicationContext applicationContext;
        
        public MealService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
    }
}