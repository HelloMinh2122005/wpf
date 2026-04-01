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
        [ObservableProperty]
        private Food? selectedFood = null;
        public HomePageViewModel(User user, INavigationService navigationService, IDialogService dialogService)
        {
            try
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
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khởi tạo HomePageViewModel: {ex.Message}");
                throw;
            }
        }

        private void LoadFoods()
        {
            try
            {
                var foodList = _foodRepository.GetFoodsByUserId(_currentUser.Id);
                Foods = new ObservableCollection<Food>(foodList);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi tải danh sách thực phẩm: {ex.Message}");
                Foods = new ObservableCollection<Food>();
            }
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
            if(SelectedFood == null)
            {
                _dialogService.ShowWarning("Vui lòng chọn một món ăn để chỉnh sửa!");
                return;
            }
            _navigationService.OpenEditFood(_currentUser, SelectedFood);
        }

        [RelayCommand]
        private void RemoveFood()
        {
            if (SelectedFood == null)
            {
                _dialogService.ShowWarning("Vui lòng chọn một món ăn để xóa!");
                return;
            }

            if (!_dialogService.ShowConfirmation($"Bạn có chắc muốn xóa món ăn '{SelectedFood.FoodName}'?", "Xác nhận xóa"))
            {
                return;
            }

            try
            {
                _foodRepository.DeleteFood(SelectedFood.FoodId);
                Foods.Remove(SelectedFood);
                LoadTotalCaloriesThisMonth();
                LoadTotalCaloriesThisWeek();
                _dialogService.ShowSuccess("Xóa món ăn thành công!", "Thành công");
                SelectedFood = null;
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Đã xảy ra lỗi: {ex.Message} khi xóa món ăn. Vui lòng thử lại sau.", "Lỗi");
            }
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
