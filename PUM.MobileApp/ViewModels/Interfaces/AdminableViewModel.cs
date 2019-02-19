using GalaSoft.MvvmLight;

namespace PUM.MobileApp.ViewModels.Interfaces
{
    public interface IAdminableViewModel
    {
        bool AdminPanelVisibility { get; set; }
    }

    public class AdminableViewModel : ViewModelBase, IAdminableViewModel
    {
        private bool adminPanelVisibility;
        public bool AdminPanelVisibility
        {
            get
            {
                return adminPanelVisibility;
            }
            set
            {
                adminPanelVisibility = value;
                RaisePropertyChanged("AdminPanelVisibility");
            }
        }
    }
}
