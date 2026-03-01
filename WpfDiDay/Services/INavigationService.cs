using WpfDiDay.Models;

namespace WpfDiDay.Services
{
    public interface INavigationService
    {
        void NavigateToHome(User user);
        void NavigateToRegister();
        void NavigateToLogin();
    }
}
