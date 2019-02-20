namespace PUM.Database.Queries
{
    public class FeeQueries
    {
        public const string GetFeesQuery = @"
SELECT CONCAT(U.FirstName, ' ', U.LastName) AS FinedUsername
    , F.FeeID
    , F.UserID
    , F.Date AS FinnedDate
FROM dbo.Fees F
INNER JOIN dbo.Users U ON F.UserID = U.UserID";

        public const string GetUserFeesQuery = @"
SELECT CONCAT(U.FirstName, ' ', U.LastName) AS FinedUsername
    , F.FeeID
    , F.UserID
    , F.Date AS FinnedDate
FROM dbo.Fees F
INNER JOIN dbo.Users U ON F.UserID = U.UserID
WHERE F.UserID = @UserID";
    }
}
