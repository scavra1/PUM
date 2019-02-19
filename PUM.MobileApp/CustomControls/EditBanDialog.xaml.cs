using PUM.SharedModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PUM.MobileApp.CustomControls
{
    public sealed partial class EditBanDialog : ContentDialog
    {
        public EditBanDialog()
        {
            this.InitializeComponent();
        }

        public EditBanDialog(Ban ban)
        {
            this.InitializeComponent();
            currentBan = ban;

            this.Title = ban == null ? "Create new ban" : "Edit ban";
            this.reasonTextBox.Text = ban.Reason;
            this.expirationDatePicker.Date = ban.ExpirationDate;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private Ban currentBan;
    }
}
