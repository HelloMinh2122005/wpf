using System.Windows.Controls;
using WpfDiDay.Services;
using WpfDiDay.ViewModels.Onboarding;

namespace WpfDiDay.Views.Onboarding
{
    public partial class RegisterPage : Page
    {
        private RegisterPageViewModel _viewModel = null!;

        public RegisterPage()
        {
            InitializeComponent();

            var navigationService = new WpfNavigationService(this);
            var dialogService = new WpfDialogService();

            _viewModel = new RegisterPageViewModel(navigationService, dialogService);
            DataContext = _viewModel;
        }

        private void Password_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.Password = pbPassword.Password;
        }

        private void ConfirmPassword_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.ConfirmPassword = pbConfirmPassword.Password;
        }
    }
}
  