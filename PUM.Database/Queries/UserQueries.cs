namespace PUM.Database.Queries
{
    public class UserQueries
    {
        public const string FindUser = @"
SELECT UserID,
       FirstName,
       Lastname,
       RoomNumber,
       IsAdmin
FROM dbo.Users
WHERE Username = @Username
    AND Password = @Password";
    }
}
