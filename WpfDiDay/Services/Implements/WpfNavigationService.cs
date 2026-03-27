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
            this._page = page;
        }

        public void NavigateToHome(User user)
        {
            this._page.NavigationService?.Navigate(new HomePage(user));
        }
        public void NavigateToRegister()
        {
            this._page.NavigationService?.Navigate(new RegisterPage());
        }

        public void NavigateToLogin()
        {
            this._page.NavigationService?.GoBack();
        }
        public void NavigateToAddFood(User? user)
        {
            this._page.NavigationService?.Navigate(new AddFoodPage(user));
        }
    }
}