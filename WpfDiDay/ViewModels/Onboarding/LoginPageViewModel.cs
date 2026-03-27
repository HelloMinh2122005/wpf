using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDiDay.Repositories;
using WpfDiDay.Services;

namespace WpfDiDay.ViewModels.Onboarding
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly UserRepository _userRepository = new();
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private string userName = "";

        [ObservableProperty]
        private string password = "";

        public LoginPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [RelayCommand]
        private void NavigateToHome()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                _dialogService.ShowWarning("Vui lòng nhập tên đăng nhập!");
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                _dialogService.ShowWarning("Vui lòng nhập mật khẩu!");
                return;
            }

            var user = _userRepository.FindByUserName(UserName);
            if (user == null)
            {
                _dialogService.ShowError("Tên đăng nhập không tồn tại!", "Lỗi đăng nhập");
                return;
            }

            if (user.Password != Password)
            {
                _dialogService.ShowError("Mật khẩu không chính xác!", "Lỗi đăng nhập");
                return;
            }

            _dialogService.ShowSuccess($"Đăng nhập thành công! Chào mừng {user.FirstName} {user.LastName}");
            _navigationService.NavigateToHome(user);
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            _navigationService.NavigateToRegister();
        }
    }
}
