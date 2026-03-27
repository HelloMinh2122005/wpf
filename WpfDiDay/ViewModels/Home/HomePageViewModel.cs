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
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly FoodRepository _foodRepository;
        private readonly User _currentUser;

        [ObservableProperty]
        private string welcomeText = "";

        [ObservableProperty]
        private ObservableCollection<Food> foods = new();
        [ObservableProperty]
        private long totalCaloriesThisWeek = 0;
        [ObservableProperty]
        private long totalCaloriesThisMonth = 0;
        public HomePageViewModel(User user, INavigationService navigationService, IDialogService dialogService)
        {
            _currentUser = user; 
            _navigationService = navigationService;
            _dialogService = dialogService;
            _foodRepository = new FoodRepository();
            welcomeText = $"Chào mừng trở lại, {user.FirstName} {user.LastName} 👋";
            LoadFoods();
            LoadTotalCaloriesThisMonth();
            LoadTotalCaloriesThisWeek();
        }

        private void LoadFoods()
        {
            var foodList = _foodRepository.GetFoodsByUserId(_currentUser.Id);
            Foods = new ObservableCollection<Food>(foodList);
        }

        private void LoadTotalCaloriesThisWeek()
        {
            TotalCaloriesThisWeek = _foodRepository.GetTotalCaloriesThisWeek(_currentUser.Id);
        }
        private void LoadTotalCaloriesThisMonth()
        {
            TotalCaloriesThisMonth = _foodRepository.GetTotalCaloriesThisMonth(_currentUser.Id);
        }
        [RelayCommand]
        private void OpenAddFood()
        {
            _navigationService.OpenAddFood(_currentUser);
        }
        [RelayCommand]
        private void OpenEditFood()
        {
            _navigationService.OpenEditFood(_currentUser);
        }
        [RelayCommand]
        private void RemoveFood()
        {

        }

        [RelayCommand]
        private void Logout()
        {
            if (_dialogService.ShowConfirmation("Đăng xuất khỏi hệ thống?", "Đăng xuất"))
            {
                _navigationService.NavigateToLogin();
            }
        }

        
    }
}
