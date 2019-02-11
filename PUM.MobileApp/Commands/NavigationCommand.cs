using CommonServiceLocator;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows.Input;

namespace PUM.MobileApp.Commands
{
    public class NavigationCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NavigationCommand(string navigatedView)
        {
            this.navigatedView = navigatedView;
            this.navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            navigationService.NavigateTo(navigatedView);
        }

        private string navigatedView;
        private INavigationService navigationService;
    }
}
