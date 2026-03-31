using Microsoft.EntityFrameworkCore.Sqlite.Storage.Json.Internal;
using WpfDiDay.Data;
using WpfDiDay.Models;

namespace WpfDiDay.Repositories
{
    public class FoodRepository
    {
        private readonly AppDbContext _context;
        
        public FoodRepository()
        {
            _context = new AppDbContext();
        }

        public void Save(Food food)
        {
            if (food == null)
                throw new ArgumentNullException(nameof(food));
            _context.Foods.Add(food);
            _context.SaveChanges();
        }
        public void Update(Food food)
        {
            if (food == null)
                throw new ArgumentNullException(nameof(food));
            Food? existing_food = _context.Foods.FirstOrDefault(f=>f.FoodID == food.FoodID);
            if (existing_food == null)
                throw new InvalidOperationException("Food not found");
            existing_food.FoodName = food.FoodName;
            existing_food.WhenEaten = food.WhenEaten;
            existing_food.Calories = food.Calories;
            _context.SaveChanges();
        }
        public void Delete(Food food)
        {
            if (food == null)
                throw new ArgumentNullException(nameof(food));
            Food? existing_food = null;
            if (food.FoodID != 0)
                existing_food = _context.Foods.FirstOrDefault(f => f.FoodID == food.FoodID);
            else if (!String.IsNullOrEmpty(food.FoodName))
                existing_food = _context.Foods.FirstOrDefault(f => f.FoodName == food.FoodName);

            if (existing_food != null)
                _context.Foods.Remove(food);
            else
                Console.WriteLine("Food: Not found. No deletion performed");
            _context.SaveChanges();
        }
        public bool ExistsByFoodName(string _foodname)
        {
            return _context.Foods.Any(f => f.FoodName == _foodname);
        }

        public Food? FindByFoodName(string _foodname)
        {
            return _context.Foods.FirstOrDefault(f => f.FoodName == _foodname);
        }
        public List<Food>? GetFoodsByUserId(int userId)
        {
            return _context.Foods.Where(f => f.UserId == userId).ToList();
        }
        public int GetSumCalo()
        {
            return 0;
        }
    }
}
