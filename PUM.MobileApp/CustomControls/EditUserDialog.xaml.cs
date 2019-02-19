using PUM.SharedModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class EditUserDialog : ContentDialog
    {
        public EditUserDialog()
        {
            this.InitializeComponent();
        }

        public EditUserDialog(User user)
        {
            this.InitializeComponent();

            if (user == null)
            {
                Title = "Create New User";
            }
            else
            {
                Title = "Edit User";
                this.user = user;
                this.firstNameTextBox.Text = user.FirstName;
                this.lastNameTextBox.Text = user.LastName;
                this.roomNumberTextBox.Text = user.RoomNumber.ToString();
                this.adminCheckBox.IsChecked = user.IsAdmin;
            }
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var uri = new Uri("http://localhost/api/users/getbans");
            var client = new HttpClient();

            if (!ValidateForm())
            {
                var currentUser = new User
                {
                    FirstName = this.firstNameTextBox.Text,
                    LastName = this.lastNameTextBox.Text,
                    IsAdmin = this.adminCheckBox.IsChecked.Value,
                    RoomNumber = int.Parse(this.roomNumberTextBox.Text)
                };

                if (user == null)
                {
                    // post new user
                }
                else
                {
                    currentUser.UserID = user.UserID;
                    // post edit existing user
                }
            }
        }

        private bool ValidateForm()
        {
            var error = false;
            if (string.IsNullOrWhiteSpace(this.firstNameTextBox.Text))
            {
                this.firstNameDataError.Text = "This field cannot be empty.";
                this.firstNameTextBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                error = true;
            }
            else
            {
                this.firstNameDataError.Text = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(this.lastNameTextBox.Text))
            {
                this.lastNameDataError.Text = "This field cannot be empty.";
                this.lastNameTextBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                error = true;
            }
            else
            {
                this.roomNumberDataError.Text = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(this.roomNumberTextBox.Text))
            {
                this.roomNumberDataError.Text = "This field cannot be empty.";
                this.roomNumberTextBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                error = true;
            }
            else if (!int.TryParse(this.roomNumberTextBox.Text, out int value))
            {
                this.roomNumberDataError.Text = "This field must be a valid integer number";
                this.roomNumberTextBox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                error = true;
            }
            else
            {
                this.roomNumberDataError.Text = string.Empty;
            }

            return error;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private User user;
    }
}
