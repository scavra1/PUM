using PUM.SharedModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace PUM.MobileApp.CustomControls
{
    public sealed partial class ReservationControl : UserControl
    {
        public ReservationControl()
        {
            this.InitializeComponent();
            mainGrid.DataContext = this;
        }

        public Reservation Reservation
        {
            get
            {
                return (Reservation)GetValue(ReservationProperty);
            }
            set
            {
                SetValue(ReservationProperty, value);
            }
        }

      
        public ICommand ReserveCommand
        {
            get
            {
                return (ICommand)GetValue(ReservationCommandProperty);
            }
            set
            {
                SetValue(ReservationCommandProperty, value);
            }
        }


        public static readonly DependencyProperty ReservationCommandProperty = DependencyProperty.Register("ReserveCommand", typeof(ICommand), typeof(ReservationControl), new PropertyMetadata(null));

        public static readonly DependencyProperty ReservationProperty = DependencyProperty.Register("Reservation", typeof(Reservation), typeof(ReservationControl), new PropertyMetadata(null));
    }
}
