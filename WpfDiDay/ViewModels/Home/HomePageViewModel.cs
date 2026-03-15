using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDiDay.Models;
using WpfDiDay.Services;

namespace WpfDiDay.ViewModels.Home
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private string welcomeText = "";

        public HomePageViewModel(User user, INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            WelcomeText = $"Chào mừng trở lại, {user.FirstName} {user.LastName} 👋";
        }

        [RelayCommand]
        private void OpenAddFood()
        {
            _navigationService.OpenAddFood();
        }
        [RelayCommand]
        private void OpenEditFood()
        {
            _navigationService.OpenEditFood();
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
