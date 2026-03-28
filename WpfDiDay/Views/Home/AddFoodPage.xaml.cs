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
using WpfDiDay.Models;
using WpfDiDay.Services.Implements;
using WpfDiDay.ViewModels.Home;

namespace WpfDiDay.Views.Home
{
    /// <summary>
    /// Interaction logic for AddFoodPage.xaml
    /// </summary>
    public partial class AddFoodPage : Page
    {
        public AddFoodPage(User? user)
        {
            InitializeComponent();
            var navigationService = new WpfNavigationService(this);
            var dialogService = new WpfDialogService();

            DataContext = new AddFoodPageViewModel(navigationService, dialogService, user);
        }
    }
}
