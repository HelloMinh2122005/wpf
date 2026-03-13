using System.Windows.Navigation;
using WpfDiDay.Models;
using WpfDiDay.Views.Home;
using WpfDiDay.Views.Onboarding;

namespace WpfDiDay.Services
{
    public class WpfNavigationService : INavigationService
    {
        private readonly Func<NavigationService?> _navigationServiceFactory;

        public WpfNavigationService(Func<NavigationService?> navigationServiceFactory)
        {
            _navigationServiceFactory = navigationServiceFactory;
        }

        public void NavigateToHome(User user)
        {
            _navigationServiceFactory()?.Navigate(new HomePage(user));
        }

        public void NavigateToRegister()
        {
            _navigationServiceFactory()?.Navigate(new RegisterPage());
        }

        public void NavigateToLogin()
        {
            _navigationServiceFactory()?.GoBack();
        }
    }
}
