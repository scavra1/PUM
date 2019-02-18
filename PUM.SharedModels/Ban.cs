namespace PUM.SharedModels
{
    using System;
    public class Ban
    {
        public long BanID { get; set; }

        public string BannedUsername { get; set; }

        public string Reason { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool Active { get; set; }
    }
}
