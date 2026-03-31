using WpfDiDay.Models;

namespace WpfDiDay.Services
{
    public interface INavigationService
    {
        void NavigateToHome(User user);
        void NavigateToRegister();
        void NavigateToLogin();
        void NavigateToAddFood(User? user); // AddFoodCommand
        void NavigateToAddFood(User? user, Food? selected_food = null); // EditFoodCommand
    }
}
