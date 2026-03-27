using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfDiDay.Models;
using WpfDiDay.Services;

namespace WpfDiDay.ViewModels.Home
{
    partial class AddFoodPageViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        [ObservableProperty]
        private string welcomeText = "";

        public AddFoodPageViewModel(INavigationService navigationService, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        [RelayCommand]
        private void BackHome(User user)
        {
            _navigationService.NavigateToHome(user);
        }
    }
}
