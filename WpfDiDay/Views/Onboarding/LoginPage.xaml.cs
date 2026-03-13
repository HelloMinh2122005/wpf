using System.Windows.Controls;
using WpfDiDay.Services;
using WpfDiDay.ViewModels.Onboarding;

namespace WpfDiDay.Views.Onboarding
{
    public partial class LoginPage : Page
    {
        private LoginPageViewModel _viewModel = null!;

        public LoginPage()
        {
            InitializeComponent();

            var navigationService = new WpfNavigationService(() => this.NavigationService);
            var dialogService = new WpfDialogService();

            _viewModel = new LoginPageViewModel(navigationService, dialogService);
            DataContext = _viewModel;
        }

        private void Password_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.Password = pbPassword.Password;
        }
    }
}
