using System.Windows;
using System.Windows.Controls;
using WpfDiDay.Services.Implements;
using WpfDiDay.ViewModels.Onboarding;

namespace WpfDiDay.Views.Onboarding
{
    public partial class LoginPage : Page
    {
        private readonly LoginPageViewModel _viewModel = null!;

        public LoginPage()
        {
            InitializeComponent();

            var navigationService = new WpfNavigationService(this);
            var dialogService = new WpfDialogService();

            _viewModel = new LoginPageViewModel(navigationService, dialogService);
            DataContext = _viewModel;
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = pbPassword.Password;
        }
    }
}
