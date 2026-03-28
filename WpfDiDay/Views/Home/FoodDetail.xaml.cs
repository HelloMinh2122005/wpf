using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfDiDay.Services.Implements;
using WpfDiDay.ViewModels.Home;
using WpfDiDay.ViewModels.Onboarding;
using WpfDiDay.Models;


namespace WpfDiDay.Views.Home
{
    public partial class FoodDetail : Page
    {
        private readonly FoodDetailViewModel _viewModel = null!;
        public FoodDetail(User user)
        {
            InitializeComponent();

            var navigationService = new WpfNavigationService(this);
            var dialogService = new WpfDialogService();

            _viewModel = new FoodDetailViewModel(user, navigationService, dialogService);
            DataContext = _viewModel;
        }

    }
}
