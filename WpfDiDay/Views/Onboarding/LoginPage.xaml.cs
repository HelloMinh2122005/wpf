using System.Windows.Controls;
using WpfDiDay.ViewModels.Onboarding;

namespace WpfDiDay.Views.Onboarding
{
    public partial class LoginPage : Page
    {
        private readonly LoginPageViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            _viewModel = new LoginPageViewModel();
            _viewModel.SetPage(this);
            DataContext = _viewModel;
        }
    }
}
