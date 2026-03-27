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


        [ObservableProperty]
        private string foodName = "";
        [ObservableProperty]
        private DateTime foodEadtingTime = DateTime.Now;
        [ObservableProperty]
        private long foodCalories = 0;
        public FoodDetailViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _foodRepository = new FoodRepository();
            _navigationService = navigationService;
            _dialogService = dialogService;
        }
        [RelayCommand]
        private void SaveFood()
        {
            if(string.IsNullOrWhiteSpace(FoodName))
            {
                _dialogService.ShowWarning("Vui lòng nhập tên món ăn!", "Thông báo");
                return;
            }
            if(FoodCalories <= 0)
            {
                _dialogService.ShowWarning("Vui lòng nhập số calo hợp lệ!", "Thông báo");
                return;
            }
            _dialogService.ShowSuccess($"Lưu món ăn thành công! Tên: {FoodName}, Thời gian: {FoodEadtingTime}, Calo: {FoodCalories}");
            _navigationService.NavigateToHome(null!);
        }
    }
}
