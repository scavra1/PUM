using System;

namespace PUM.SharedModels
{
    public class Reservation
    {
        public long? ReservationID { get; set; }

        public string Person { get; set; }

        public long? UserID { get; set; }

        public DateTime Date { get; set; }

        public int DateKey { get; set; }

        public int HourKey { get; set; }

        public string HourDescription { get; set; }

        public bool Fee { get; set; }
    }
}
