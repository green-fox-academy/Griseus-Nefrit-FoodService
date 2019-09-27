using System.Threading.Tasks;
using FoodService.Models.RequestModels.Restaurant;
using FoodService.Models;
using FoodService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FoodService.Services.MealService
{
    public class MealService : IMealService
    {
        private readonly ApplicationContext applicationContext;

        public MealService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        
        public async Task SaveMealAsync(AddMealRequest model)
        {
            var meal = new Meal();
            meal.Price = new Price();
            meal.Restaurant = await applicationContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == model.RestaurantId);
            meal.Description = model.Description;
            meal.Price.Amount = model.Price.Amount;
            meal.Price.Currency = model.Price.Currency;
            meal.Name = model.Name;
            meal.Restaurant.RestaurantId = model.RestaurantId;
            await applicationContext.Meals.AddAsync(meal);
            await applicationContext.SaveChangesAsync();
        }
        
        public async Task DeleteMeal(long ID)
        {
            var meal = await GetMealByIdAsync(ID);
            if (meal != null)
            {
                applicationContext.Meals.Remove(meal);
                applicationContext.SaveChanges();
            }
        }
        
        public async Task<Meal> GetMealByIdAsync(long MealId)
        {
            var meal = await applicationContext.Meals.Include(m => m.Restaurant).Include(p => p.Price).FirstOrDefaultAsync(m => m.MealId == MealId);
            if (meal == null)
            {
                return null;
            }
            return meal;
        }

        public async Task<EditRestaurantViewModel> CreateViewModel(long Id)
        {
            var viewmodel = new EditRestaurantViewModel();
            var meal = await GetMealByIdAsync(Id);
            viewmodel.Restaurant = meal.Restaurant;
            viewmodel.AddMealRequest.Name = meal.Name;
            viewmodel.AddMealRequest.Description = meal.Description;
            viewmodel.AddMealRequest.Price.Amount = meal.Price.Amount;
            viewmodel.AddMealRequest.Price.Currency = meal.Price.Currency;
            return viewmodel;
        }
        
        public async Task UpdateMealAsync(AddMealRequest model)
        {
            var meal = new Meal();
            meal.Price = new Price();
            meal.Restaurant = await applicationContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == model.RestaurantId);
            meal.Description = model.Description;
            meal.Price.Amount = model.Price.Amount;
            meal.Price.Currency = model.Price.Currency;
            meal.Name = model.Name;
            meal.Restaurant.RestaurantId = model.RestaurantId;
            applicationContext.Meals.Update(meal);
           // await applicationContext.Meals.AddAsync(meal);
            await applicationContext.SaveChangesAsync();
        }

        public async Task EditAsync(long Id, AddMealRequest addMealRequest)
        {
            var meal = await GetMealByIdAsync(Id);
            meal.Price = new Price();
            meal.Restaurant = await applicationContext.Restaurants.Include(t => t.Meals).ThenInclude(m => m.Price)
                .FirstOrDefaultAsync(t => t.RestaurantId == addMealRequest.RestaurantId);
            meal.Description = addMealRequest.Description;
            meal.Price.Amount = addMealRequest.Price.Amount;
            meal.Price.Currency = addMealRequest.Price.Currency;
            meal.Name = addMealRequest.Name;
            meal.Restaurant.RestaurantId = addMealRequest.RestaurantId;
            await applicationContext.SaveChangesAsync();
        }
    }
}