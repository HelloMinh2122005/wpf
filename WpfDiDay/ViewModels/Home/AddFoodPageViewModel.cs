using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDiDay.Data;
using WpfDiDay.Models;
using WpfDiDay.Repositories;
using WpfDiDay.Services;

namespace WpfDiDay.ViewModels.Home
{
    partial class AddFoodPageViewModel : ObservableObject
    {
        // Fields
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly FoodRepository foodRepository = new FoodRepository();
        private readonly User? _user;
		private readonly Food? _editing_food;

		// Bindable properties 
		[ObservableProperty]
        private string foodname = ""; // -> Foodname
        [ObservableProperty]
        private string day = "";      // -> Day
        [ObservableProperty]
        private string month = "";    // -> Month
        [ObservableProperty]
        private string year = "";     // -> Year
        [ObservableProperty]
        private string calories = ""; // Calories

        // Constructor of AddFoodPageVM for adding food.
        public AddFoodPageViewModel(INavigationService navigationService, IDialogService dialogService, 
                                    User? user)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _user = user;
        }
        
        // Constructor of AddFoodPageVM for editing food.
		public AddFoodPageViewModel(INavigationService navigationService, IDialogService dialogService, 
                                    User? user, Food? selected_food)
		{
			_navigationService = navigationService;
			_dialogService = dialogService;
			_user = user;
            _editing_food = selected_food;
            if (_editing_food != null)
            {
                this.Foodname = _editing_food.FoodName ?? "";
                this.Calories = _editing_food.Calories.ToString();
                if (!String.IsNullOrEmpty(_editing_food.WhenEaten))
                {
                    // WhenEaten = "DD/MM/YYYY" -> parts = {"DD", "MM", "YYYY"}
                    var parts = _editing_food.WhenEaten.Split('/');
                    if (parts.Length >= 3)
                    {
                        this.day = parts[0];
						this.month = parts[1];
						this.year = parts[2];
					}
                }
            }
		}

		[RelayCommand]
        private void BackHome()
        {
            if (_user == null)
            { 
                _dialogService.ShowError("Unable to go back home", "Error");
                return;
            }
            _navigationService.NavigateToHome(_user);
        }
        [RelayCommand]
        private void SaveFood() // AddFoodCommand + EditFoodCommand
        {
            if(string.IsNullOrEmpty(Foodname))
            {
                _dialogService.ShowWarning("Enter a food name", "Validation");
                return;
            }
            
            if (_user == null)
            {
                _dialogService.ShowError("No avaiable user", "Error");
                return;
            }

            string eatingdate = $"{Day}/{Month}/{Year}";

            if (!int.TryParse(Calories, out var cal))
                cal = 0;

            if (_editing_food != null)
            {
                _editing_food.FoodName = Foodname;
                _editing_food.WhenEaten = eatingdate;
                _editing_food.Calories = cal;
                foodRepository.Update(_editing_food);
                _dialogService.ShowSuccess("Food successfully updated", "Success");
            }
            else
            {
                var added_food = new Food
                {
                    UserId = _user.UserId,   // add_food.UserID = user.UserId
                    FoodName = Foodname,    // added_food.FoodName = Foodname
                    WhenEaten = eatingdate, // added_food.WhenEaten = eatingdate
                    Calories = cal          // added_food.Calories = cal
                };

                foodRepository.Save(added_food);
                _dialogService.ShowSuccess("Saved Food", "Success");
            }

            // Finishing ADDING or EDITING food -> Back home !!!
			_navigationService.NavigateToHome(_user);
		}
    }
}
