namespace PUM.SharedModels
{
    public class User
    {
        public long UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int RoomNumber { get; set; }

        public bool IsAdmin { get; set; }
    }
}
