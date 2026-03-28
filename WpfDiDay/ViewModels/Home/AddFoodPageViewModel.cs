using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDiDay.Models;
using WpfDiDay.Repositories;
using WpfDiDay.Services;

namespace WpfDiDay.ViewModels.Home
{
    partial class AddFoodPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly FoodRepository foodRepository = new FoodRepository();

        [ObservableProperty]
        private string welcomeText = "";
        
        // Bindable properties 
        [ObservableProperty]
        private string foodname = "";
        [ObservableProperty]
        private string day = "";
        [ObservableProperty]
        private string month = "";
        [ObservableProperty]
        private string year = "";
        [ObservableProperty]
        private string calories = "";
        public AddFoodPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [RelayCommand]
        private void BackHome(User user)
        {
            _navigationService.NavigateToHome(user);
        }
        [RelayCommand]
        private void SaveFood(User user)
        {
            if(string.IsNullOrEmpty(Foodname))
            {
                _dialogService.ShowWarning("Enter a food name", "Validation");
                return;
            }

            string eatingdate = $"{Day}/{Month}/{Year}";

            if (!int.TryParse(Calories, out var cal))
                cal = 0;

            var added_food = new Food
            {
                UserId = user.UserId,   // add_food.UserID = user.UserId
                FoodName = Foodname,    // added_food.FoodName = Foodname
                WhenEaten = eatingdate, // added_food.WhenEaten = eatingdate
                Calories = cal          // added_food.Calories = cal
            };

            foodRepository.Save(added_food);
            _dialogService.ShowSuccess("Saved Food", "Success");
            _navigationService.NavigateToHome(user);
        }
    }
}
