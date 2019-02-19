using GalaSoft.MvvmLight;
using System;

namespace PUM.MobileApp.ViewModels.Interfaces
{
    public interface IAppBarableViewModel
    {
        string CurrentView { get; set; }
    }

    public class AppBarableViewModel : ViewModelBase, IAppBarableViewModel
    {
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
