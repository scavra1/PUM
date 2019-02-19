namespace PUM.Database.Queries
{
    public class ReservationQueries
    {
        public const string ReserveQuery = @"
INSERT INTO dbo.Reservations
(
    UserID,
    DateKey,
    HourKey,
    Fee
)
VALUES
(
    @UserID,
    @DateKey,
    @HourKey,
    0
)";

        public const string GetReservations = @"
SELECT R.ReservationID
        , CONCAT (U.FirstName, ' ', U.LastName) AS Person
        , R.UserID
        , R.[Date]
        , R.DateKey
        , R.HourKey
        , R.Fee
FROM dbo.Reservations R
INNER JOIN dbo.Users U ON U.UserID = R.UserID
WHERE R.DateKey = @DateKey";

        public const string GetUserReservations = @"
SELECT R.ReservationID
        , CONCAT (U.FirstName, ' ', U.LastName) AS Person
        , R.UserID
        , R.[Date]
        , R.DateKey
        , R.HourKey
        , R.Fee
FROM dbo.Reservations R
INNER JOIN dbo.Users U ON U.UserID = R.UserID
WHERE R.UserID = @UserID";



        public const string CheckUserDailyReservationsQuery = @"
SELECT
CASE 
	WHEN EXISTS ( SELECT ReservationID 
				  FROM dbo.Reservations
				  WHERE UserID = @UserID
					AND DateKey = @DateKey
                )
	THEN 0
	ELSE 1
END";

        public const string CheckUserWeeklyReservationsQuery = @"
SELECT
CASE 
	WHEN EXISTS ( SELECT ReservationID 
				  FROM dbo.Reservations
				  WHERE UserID = @UserID
					AND DateKey BETWEEN @StartDateKey AND @EndDateKey
				)
	THEN 0
	ELSE 1
END";


        public const string DeleteReservation = @"
DELETE
FROM dbo.Reservations
WHERE ReservationID = @ReservationID";
    }
}

