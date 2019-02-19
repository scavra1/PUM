using GalaSoft.MvvmLight;
using PUM.MobileApp.Commands.CommonCommands;
using System.Windows.Input;

namespace PUM.MobileApp.ViewModels.Interfaces
{
    public interface IRefreshableViewModel
    {
        ICommand RefreshViewCommand { get; }

        void RefreshView();
    }

    public class RefreshableViewModel : ViewModelBase, IRefreshableViewModel
    {
        private ICommand refreshViewCommand;
        public ICommand RefreshViewCommand
        {
            get
            {
                if (refreshViewCommand == null)
                    refreshViewCommand = new RefreshViewCommand(this);

                return refreshViewCommand;
            }
        }

        public void RefreshView()
        {
            throw new System.NotImplementedException();
        }
    }
}
