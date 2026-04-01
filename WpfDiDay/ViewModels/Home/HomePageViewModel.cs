using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using WpfDiDay.Models;
using WpfDiDay.Repositories;
using WpfDiDay.Services;

namespace WpfDiDay.ViewModels.Home
{
    public partial class HomePageViewModel : ObservableObject
    {
        // Fields
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly User? _user = null;
        private readonly FoodRepository _foodRepository = new();

        // Bindable properties
        [ObservableProperty]
        private string welcomeText = "";                    // -> WelcomeText
        [ObservableProperty]
        private ObservableCollection<Food> foodLog = new(); // -> FoodLog
        [ObservableProperty]
        private Food? selectedFood;                         // -> SelectedFood
        [ObservableProperty]
        private string totalCalories = "";                  // -> TotalCalories
        [ObservableProperty]
        private string healthStatus = "";                  // -> HealthStatus

        public HomePageViewModel(User user, INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _user = user;

            Regenerate();
            WelcomeText = (_user != null) ? $"Chào mừng trở lại, {_user.FirstName} {_user.LastName}" 
                                            : "Chào mừng!!";
        }
        private void LoadFoodLog()
        {
            if (_user == null)
                return;
            var foodlist = _foodRepository.GetFoodsByUserId(_user.UserId);
            if(foodlist != null)
                FoodLog = new ObservableCollection<Food>(foodlist);
        }
        private void LoadCaloriesSum()
        {
            if (_user == null)
                return;
            int sumCalo = _foodRepository.GetSumCalo(_user.UserId);
            TotalCalories = sumCalo.ToString();
            HealthStatus = (sumCalo > 360) ? "Dư calo" : 
                           (sumCalo == 360) ? "Đủ calo" : "Thiếu calo";
        }
        private void Regenerate()
        {
            LoadCaloriesSum();
            LoadFoodLog();
        }
        [RelayCommand]
        private void Refresh()
        {
            LoadFoodLog();
            LoadCaloriesSum();
        }

        [RelayCommand]
        private void Logout()
        {
            if (_dialogService.ShowConfirmation("Đăng xuất khỏi hệ thống?", "Đăng xuất"))
                _navigationService.NavigateToLogin();
        }

        [RelayCommand]
        private void AddFood()
        {
            _navigationService.NavigateToAddFood(_user);
        }
        [RelayCommand]
        private void EditFood()
        {
            if (SelectedFood == null)
            {
                _dialogService.ShowWarning("Chọn một món ăn để chỉnh sửa", "Warning");
                return;
            }
            _navigationService.NavigateToAddFood(_user, SelectedFood);
        }
        [RelayCommand]
        private void RemoveFood()
        {
            if (SelectedFood == null)
            {
                _dialogService.ShowWarning("Select a food to edit", "Warning");
                return;
            }
            if (_dialogService.ShowConfirmation("Bạn có thực sự muốn xóa?", "Xác nhận"))
            { 
                _foodRepository.Delete(SelectedFood);
                if(_user != null) 
                    _navigationService.NavigateToHome(_user);
            }
        }
    }   
}
