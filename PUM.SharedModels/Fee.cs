using System;

namespace PUM.SharedModels
{
    public class Fee
    {
        public long FeeID { get; set; }

        public string FinedUsername { get; set; }

        public long UserID { get; set; }

        public DateTime FinnedDate { get; set; }

        public int Amount { get; set; }
    }
}
