using System;
using System.Windows.Navigation;
using WpfDiDay.Models;
using WpfDiDay.Views.Home;
using WpfDiDay.Views.Onboarding;

namespace WpfDiDay.Services
{
    /// <summary>
    /// Concrete WPF implementation of INavigationService.
    /// Accepts a lazy factory for NavigationService because the Page's
    /// NavigationService property is null during construction and only becomes
    /// available once the page is attached to a Frame/NavigationWindow.
    /// </summary>
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
