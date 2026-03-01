using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;
using WpfDiDay.Repositories;
using WpfDiDay.Views.Home;
using WpfDiDay.Views.Onboarding;

namespace WpfDiDay.ViewModels.Onboarding
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly UserRepository userRepository = new();
        private Page? _page;

        [ObservableProperty]
        private string userName = "";

        [ObservableProperty]
        private string password = "";

        public void SetPage(Page page)
        {
            _page = page;
        }

        [RelayCommand]
        private void DangNhapButton()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = userRepository.FindByUserName(UserName);
            if (user == null)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại!", "Lỗi đăng nhập", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.Password != Password)
            {
                MessageBox.Show("Mật khẩu không chính xác!", "Lỗi đăng nhập", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show($"Đăng nhập thành công! Chào mừng {user.FirstName} {user.LastName}", 
                "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

            if (_page?.NavigationService != null)
            {
                _page.NavigationService.Navigate(new HomePage(user));
            }
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            if (_page?.NavigationService != null)
            {
                _page.NavigationService.Navigate(new RegisterPage());
            }
        }
    }
}
