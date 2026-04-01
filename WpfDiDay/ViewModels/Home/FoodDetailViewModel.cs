using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDiDay.Models;
using WpfDiDay.Repositories;
using WpfDiDay.Services;

namespace WpfDiDay.ViewModels.Home
{
    public partial class FoodDetailViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;
        private readonly FoodRepository _foodRepository;
        private readonly User _currentUser;
        private readonly Food? _editingFood;
        private readonly bool isEditMode; //Edit or Add food


        [ObservableProperty]
        private string foodName = "";
        [ObservableProperty]
        private DateTime foodEadtingTime = DateTime.Now;
        [ObservableProperty]
        private long foodCalories = 0;
        public FoodDetailViewModel(User user, INavigationService navigationService, IDialogService dialogService, Food? editingFood = null)
        {
            try
            {
                _currentUser = user;
                _foodRepository = new FoodRepository();
                _navigationService = navigationService;
                _dialogService = dialogService;
                _editingFood = editingFood;

                if(editingFood != null)
                {
                    isEditMode = true;
                    FoodName = editingFood.FoodName;
                    FoodEadtingTime = editingFood.FoodEatingTime;
                    FoodCalories = editingFood.FoodCalories;
                }
                else
                {
                    isEditMode = false;
                    FoodEadtingTime = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khởi tạo FoodDetailViewModel: {ex.Message}");
            }

        }


        [RelayCommand]
        private void SaveFood()
        {
            if(string.IsNullOrWhiteSpace(FoodName))
            {
                _dialogService.ShowWarning("Vui lòng nhập tên món ăn!", "Thông báo");
                return;
            }
            if(FoodCalories <= 0 || FoodCalories >= 5000)
            {
                _dialogService.ShowWarning("Vui lòng nhập số calo hợp lệ!", "Thông báo");
                return;
            }
            
            try
            {
                if (isEditMode && _editingFood != null)
                {
                    // Update existing food
                    _editingFood.FoodName = FoodName;
                    _editingFood.FoodEatingTime = FoodEadtingTime;
                    _editingFood.FoodCalories = FoodCalories;

                    _foodRepository.UpdateFood(_editingFood);
                    _dialogService.ShowSuccess("Cập nhật thành công!", "Thành công");
                }
                else
                {
                    // Create new food
                    var newFood = new Food
                    {
                        UserId = _currentUser.Id,
                        FoodName = FoodName,
                        FoodEatingTime = FoodEadtingTime,
                        FoodCalories = FoodCalories
                    };

                    _foodRepository.SaveFood(newFood);
                    _dialogService.ShowSuccess("Lưu thành công!", "Thành công");
                }

                _navigationService.NavigateToHome(_currentUser);
            }
            catch (Exception ex)
            {
                _dialogService.ShowError($"Lỗi {ex.Message}", "Lỗi");
                throw;
            }
        }
        [RelayCommand]
        private void Back()
        {
            _navigationService.NavigateToHome(_currentUser);
        }
    }
}
