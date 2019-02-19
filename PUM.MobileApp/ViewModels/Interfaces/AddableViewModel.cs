using GalaSoft.MvvmLight;
using PUM.MobileApp.Commands.CommonCommands;
using System.Windows.Input;

namespace PUM.MobileApp.ViewModels.Interfaces
{
    public interface IAddableViewModel
    {
        ICommand AddItemCommand { get; }

        void AddItem();
    }

    public class AddableViewModel : ViewModelBase, IAddableViewModel
    {
        private ICommand addItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (addItemCommand == null)
                    addItemCommand = new AddItemCommand(this);

                return addItemCommand;
            }
        }

        public void AddItem()
        {
            throw new System.NotImplementedException();
        }
    }
}
