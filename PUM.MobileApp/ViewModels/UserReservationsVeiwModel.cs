using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using PUM.MobileApp.Services;
using PUM.SharedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PUM.MobileApp.ViewModels
{
    /// <summary>
    /// ViewModel class for UserReservationsView
    /// </summary>
    public class UserReservationsVeiwModel : ViewModelBase
    {
        public UserReservationsVeiwModel(IUserService userService)
        {
            UserService = userService;
            DownloadMyReservations();
        }

        #region Properties
        public IUserService UserService { get; private set; }

        private ObservableCollection<Reservation> userReservationsCollection;
        public ObservableCollection<Reservation> UsersReservationCollection
        {
            get
            {
                return userReservationsCollection;
            }
            set
            {
                userReservationsCollection = value;
                RaisePropertyChanged("UsersReservationCollection");
            }
        }

        private bool isWorking;
        public bool IsWorking
        {
            get
            {
                return isWorking;
            }
            set
            {
                if (isWorking != value)
                {
                    isWorking = value;
                    RaisePropertyChanged("IsWorking");
                }
            }
        }
        #endregion

        private async Task DownloadMyReservations()
        {
            IsWorking = true;

            var uriString = "http://localhost/api/reservations/getuserreservations?";
            uriString += "userID=" + UserService.CurrentUser.UserID.ToString();

            var uri = new Uri(uriString);
            var client = new HttpClient();
            var response = await client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();
            var reservations = JsonConvert.DeserializeObject<List<Reservation>>(content);

            UsersReservationCollection = new ObservableCollection<Reservation>(reservations);

            IsWorking = false;
        }
    }
}
