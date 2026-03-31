using System.Windows.Controls;
using System.Windows.Input;
using WpfDiDay.Models;
using WpfDiDay.Services.Implements;
using WpfDiDay.ViewModels.Home;

namespace WpfDiDay.Views.Home
{
    public partial class HomePage : Page
    {
        private HomePageViewModel _viewmodel;
        public HomePage(User user)
        {
            InitializeComponent();

            var navigationService = new WpfNavigationService(this);
            var dialogService = new WpfDialogService();

            _viewmodel = new HomePageViewModel(user, navigationService, dialogService);
            DataContext = _viewmodel;
        }

        private void Logout_Click(object sender, MouseButtonEventArgs e)
        {
            _viewmodel.LogoutCommand.Execute(null);
        }
        private void AddFood_Click(object sender, MouseButtonEventArgs e)
        {
            _viewmodel.AddFoodCommand.Execute(null);
        }
    }
}
