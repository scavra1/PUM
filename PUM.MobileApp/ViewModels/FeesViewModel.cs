using PUM.MobileApp.Models;
using System.Collections.ObjectModel;

namespace PUM.MobileApp.ViewModels
{
    public class FeesViewModel
    {

        public FeesViewModel()
        {
            FeesCollection = new ObservableCollection<Fee>();
        }
        public ObservableCollection<Fee> FeesCollection { get; }
    }
}
