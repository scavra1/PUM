using GalaSoft.MvvmLight;
using PUM.MobileApp.Commands;
using System.Windows.Input;

namespace PUM.MobileApp.ViewModels.Interfaces
{
    public interface IBackableViewModel : IAppBarableViewModel
    {
        ICommand BackToMainMenuCommand { get; }
    }

    public class BackableViewModel : ViewModelBase, IBackableViewModel
    {
        private ICommand backToMainMenuCommand;

        public ICommand BackToMainMenuCommand
        {
            get
            {
                if (backToMainMenuCommand == null)
                    backToMainMenuCommand = new NavigationCommand("MainMenu");

                return backToMainMenuCommand;
            }
        }

        private string currentView;
        public string CurrentView
        {
            get
            {
                return currentView;
            }
            set
            {
                currentView = value;
                RaisePropertyChanged("CurrentView");
            }
        }
    }
}
