namespace PUM.Database.Queries
{
    public class ReservationQueries
    {
        public const string ReserveQuery = @"";

        public const string GetReservations = @"
SELECT R.ReservationID
        , CONCAT (U.FirstName, U.LastName) AS Person
        , R.UserID
        , R.[Date]
        , R.DateKey
        , R.HourKey
        , R.Fee
FROM dbo.Reservations R
INNER JOIN dbo.Users U ON U.UserID = R.UserID
WHERE R.DateKey = @DateKey";

    }
}

