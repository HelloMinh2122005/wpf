using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfDiDay.Models;
using WpfDiDay.Views.Onboarding;

namespace WpfDiDay.Views.Home
{
    public partial class HomePage : Page
    {
        private readonly User _currentUser;

        public HomePage(User user)
        {
            InitializeComponent();
            _currentUser = user;
            txtWelcome.Text = $"Chào mừng trở lại, {user.FirstName} {user.LastName} 👋";
        }

        private void Logout_Click(object sender, MouseButtonEventArgs e)
        {
            var result = MessageBox.Show("Đăng xuất khỏi hệ thống?", "Đăng xuất",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                NavigationService.Navigate(new LoginPage());
            }
        }
    }
}
