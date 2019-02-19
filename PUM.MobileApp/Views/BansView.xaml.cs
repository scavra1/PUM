using PUM.MobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PUM.MobileApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BansView : Page
    {
        public BansView()
        {
            this.InitializeComponent();
        }

        private BansViewModel viewModel;
        public BansViewModel ViewModel
        {
            get
            {
                return viewModel;
            }
            set
            {
                AdminModeVisibility = value.AdminPanelVisibility ? "true" : "false";
                this.Resources.Add("AdminMode", AdminModeVisibility);
                viewModel = value;
            }
        }

        public string AdminModeVisibility { get; set; }
    }
}
