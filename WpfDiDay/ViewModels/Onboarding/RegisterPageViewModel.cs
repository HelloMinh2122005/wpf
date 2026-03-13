using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDiDay.Models;
using WpfDiDay.Repositories;
using WpfDiDay.Services;

namespace WpfDiDay.ViewModels.Onboarding
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        private readonly UserRepository _userRepository = new();
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private string firstName = "";

        [ObservableProperty]
        private string lastName = "";

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string confirmPassword = "";

        public RegisterPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [RelayCommand]
        private void DangKy()
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                _dialogService.ShowWarning("Vui lòng nhập họ!");
                return;
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                _dialogService.ShowWarning("Vui lòng nhập tên!");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                _dialogService.ShowWarning("Vui lòng nhập tên đăng nhập!");
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                _dialogService.ShowWarning("Vui lòng nhập mật khẩu!");
                return;
            }

            if (Password != ConfirmPassword)
            {
                _dialogService.ShowError("Mật khẩu nhập lại không khớp!");
                return;
            }

            if (_userRepository.ExistsByUserName(Email))
            {
                _dialogService.ShowError("Tên đăng nhập này đã được sử dụng!");
                return;
            }

            var newUser = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password
            };

            _userRepository.Save(newUser);

            _dialogService.ShowSuccess($"Tạo tài khoản thành công! Chào mừng {FirstName} {LastName}.");
            _navigationService.NavigateToLogin();
        }

        [RelayCommand]
        private void NavigateToLogin()
        {
            _navigationService.NavigateToLogin();
        }
    }
}
