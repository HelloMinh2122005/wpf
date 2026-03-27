using System.Windows.Controls;
using WpfDiDay.Models;
using WpfDiDay.Views.Home;
using WpfDiDay.Views.Onboarding;

namespace WpfDiDay.Services.Implements
{
    public class WpfNavigationService : INavigationService
    {
        private readonly Page _page;

        public WpfNavigationService(Page page)
        {
            _page = page;
        }

        public void NavigateToHome(User user)
        {
            _page.NavigationService?.Navigate(new HomePage(user));
        }

        public void NavigateToRegister()
        {
            _page.NavigationService?.Navigate(new RegisterPage());
        }

        public void NavigateToLogin()
        {
            _page.NavigationService?.GoBack();
        }

        public void OpenEditFood(User user)
        {
            _page.NavigationService?.Navigate(new FoodDetail(user));
        }
        public void OpenAddFood(User user)
        {
            _page.NavigationService?.Navigate(new FoodDetail(user));
        }
    }
}