using Newtonsoft.Json;
using PUM.SharedModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
            DownloadUsers();


            if (ban == null)
            {
                this.Title = "New ban";
            }
            else
            {
                this.Title = "Edit ban";
                this.reasonTextBox.Text = ban.Reason;
                this.expirationDatePicker.Date = ban.ExpirationDate;
            }

           
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var client = new HttpClient();

            string resource = currentBan != null ? "updateBan" : "createBan";
            Ban newBan = new Ban();

            if (currentBan != null)
            {
                newBan.BanID = currentBan.BanID;
                newBan.ExpirationDate = this.expirationDatePicker.Date.Date;
                newBan.Reason = this.reasonTextBox.Text;
            }
            else
            {
                newBan.UserID = (this.userComboBox.SelectedItem as User).UserID;
                newBan.ExpirationDate = this.expirationDatePicker.Date.Date;
                newBan.Reason = this.reasonTextBox.Text;

            }
            var serialized = JsonConvert.SerializeObject(newBan);
            var requestBody = new StringContent(serialized, Encoding.UTF8, "application/json");

            var resourceAddress = "http://localhost/api/bans/" + resource;
            var response = await client.PostAsync(resourceAddress, requestBody);
            var responseContent = await response.Content.ReadAsStringAsync();
            var dialog = new MessageDialog(responseContent);
            dialog.ShowAsync();
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void DownloadUsers()
        {
            var client = new HttpClient();

            var response = client.GetAsync("http://localhost/api/users/getusers").Result;

            var content = response.Content.ReadAsStringAsync().Result;

            var users = JsonConvert.DeserializeObject<List<User>>(content);

            UserList = new ObservableCollection<User>(users);

            if (currentBan != null)
            {
                SelectedUser = UserList.First(u => u.UserID == currentBan.UserID);
                this.userComboBox.IsEnabled = false;
            }
        }

        public User SelectedUser { get; set; }

        public ObservableCollection<User> UserList { get; set; }

        private Ban currentBan;
    }
}
