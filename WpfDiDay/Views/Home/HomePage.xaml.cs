using System.Windows.Controls;
using System.Windows.Input;
using WpfDiDay.Models;
using WpfDiDay.Services;
using WpfDiDay.ViewModels.Home;

namespace WpfDiDay.Views.Home
{
    public partial class HomePage : Page
    {
        public HomePage(User user)
        {
            InitializeComponent();

            var navigationService = new WpfNavigationService(() => this.NavigationService);
            var dialogService = new WpfDialogService();

            DataContext = new HomePageViewModel(user, navigationService, dialogService);
        }

        private void Logout_Click(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is HomePageViewModel viewModel)
            {
                viewModel.LogoutCommand.Execute(null);
            }
        }
    }
}
