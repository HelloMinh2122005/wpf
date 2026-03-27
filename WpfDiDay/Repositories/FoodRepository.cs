using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void SaveFood(Food food)
        {
            _context.Foods.Add(food);
            _context.SaveChanges();
        }

        public void DeleteFood(int foodID)
        {
            var food = _context.Foods.FirstOrDefault(f => f.FoodId == foodID);
            if (food != null)
            {
                _context.Foods.Remove(food);
                _context.SaveChanges();
            }
        }

        public void UpdateFood(Food food)
        {
            _context.Foods.Update(food);
            _context.SaveChanges();
        }

        public List<Food> GetFoodsByUserId(int userId)
        {
            return _context.Foods
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.FoodEatingTime)
                .ToList();
        }

        public Food? FindByID(int foodId)
        {
            return _context.Foods.FirstOrDefault(f => f.FoodId == foodId);
        }


        public List<Food> GetFoodListThisWeek(int userId)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            return _context.Foods
                .Where(f => f.UserId == userId && f.FoodEatingTime >= sevenDaysAgo)
                .ToList();
        }
        public long GetTotalCaloriesThisWeek(int userId)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            return _context.Foods
                .Where(f => f.UserId == userId && f.FoodEatingTime >= sevenDaysAgo)
                .Sum(f => f.FoodCalories);
        }

        public List<Food> GetFoodListThisMonth(int userId)
        {
            var oneMonthAgo = DateTime.Now.AddDays(-30);
            return _context.Foods
                .Where(f => f.UserId == userId && f.FoodEatingTime >= oneMonthAgo)
                .ToList();
        }

        public long GetTotalCaloriesThisMonth(int userId)
        {
            var oneMonthAgo = DateTime.Now.AddDays(-30);
            return _context.Foods
                .Where(f => f.UserId == userId && f.FoodEatingTime >= oneMonthAgo)
                .Sum(f => f.FoodCalories);
        }

        public long GetTotalCaloriesByMonthSelected(int userId, int month, int year)
        {
            return _context.Foods
                .Where(f => f.UserId == userId && f.FoodEatingTime.Month == month && f.FoodEatingTime.Year == year)
                .Sum(f => f.FoodCalories);
        }
    }
}
