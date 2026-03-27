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
        private readonly User? _user;

        [ObservableProperty]
        private string welcomeText = "";

        public HomePageViewModel(User user, INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            WelcomeText = (user != null) ? $"Chào mừng trở lại, {user.FirstName}, {user.LastName}" : "Chào mừng!!";
        }

        [RelayCommand]
        private void Logout()
        {
            if (_dialogService.ShowConfirmation("Đăng xuất khỏi hệ thống?", "Đăng xuất"))
            {
                _navigationService.NavigateToLogin();
            }
        }

        [RelayCommand]
        private void AddFood()
        {
            _navigationService.NavigateToAddFood(_user);
        }
    }
}
