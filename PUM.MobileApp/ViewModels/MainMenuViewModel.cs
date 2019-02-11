namespace PUM.MobileApp.ViewModels
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Views;
    using PUM.MobileApp.Commands;
    using System.Windows.Input;

    public class MainMenuViewModel : ViewModelBase
    {
        public MainMenuViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                    logoutCommand = new LogoutCommand(this);
                return logoutCommand;
            }
        }

        public ICommand NavigateBansCommand
        {
            get
            {
                if (navigateBansCommand == null)
                    navigateBansCommand = new NavigationCommand("Bans");

                return navigateBansCommand;
            }
        }

        public ICommand NavigateFeesCommand
        {
            get
            {
                if (navigateFeesCommand == null)
                    navigateFeesCommand = new NavigationCommand("Fees");

                return navigateFeesCommand;
            }
        }

        public ICommand NavigateReservationsCommand
        {
            get
            {
                if (navigateReservationsCommand == null)
                    navigateReservationsCommand = new NavigationCommand("Reservations");

                return navigateReservationsCommand;
            }
        }

        public ICommand NavigateUserReservationsCommand
        {
            get
            {
                if (navigateUserReservationsCommand == null)
                    navigateUserReservationsCommand = new NavigationCommand("UserReservations");

                return navigateUserReservationsCommand;
            }
        }

        public ICommand NavigateUserManagementCommand
        {
            get
            {
                if (navigateUserManagementCommand == null)
                    navigateUserManagementCommand = new NavigationCommand("UserManagement");

                return navigateUserManagementCommand;
            }
        }

        private ICommand navigateUserManagementCommand;

        private ICommand navigateUserReservationsCommand;

        private ICommand navigateBansCommand;

        private ICommand navigateFeesCommand;

        private ICommand navigateReservationsCommand;

        private ICommand logoutCommand;

        public INavigationService NavigationService;
    }
}
