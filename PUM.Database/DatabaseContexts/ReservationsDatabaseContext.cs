namespace PUM.Database.DatabaseContexts
{
    using PUM.Database.Queries;
    using PUM.SharedModels;
    using System;
    using System.Collections.Generic;

    public class ReservationsDatabaseContext : DatabaseContext
    {
        public void ReserveForDay(long userId, DateTime dateTime)
        {
        }

        public List<Reservation> GetReservations(int dateKey)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("DateKey", dateKey);
            return Query<Reservation>(ReservationQueries.GetReservations, parameters);
        }

        public List<Reservation> GetUserReservations(long userID)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("UserID", userID);

            return Query<Reservation>(ReservationQueries.GetUserReservations, parameters);
        }

        public bool CheckDailyReservationForUser(long userID, int dateKey)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("UserID", userID);
            parameters.Add("DateKey", dateKey);

            return ExecuteScalar<bool>(ReservationQueries.CheckUserDailyReservationsQuery, parameters);
        }

        public bool CheckWeeklyReservation(long? userID, int startDateKey, int endDateKey)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("UserID", userID);
            parameters.Add("StartDateKey", startDateKey);
            parameters.Add("EndDateKey", endDateKey);

            return ExecuteScalar<bool>(ReservationQueries.CheckUserWeeklyReservationsQuery, parameters);
        }

        public void ReserveDateForUser(long userID, int reservationDateKey, int reservationHourKey)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("UserID", userID);
            parameters.Add("DateKey", reservationDateKey);
            parameters.Add("HourKey", reservationHourKey);

            Execute(ReservationQueries.ReserveQuery, parameters);
        }

        public void DeleteReservation(long reservationID)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("ReservationID", reservationID);

            Execute(ReservationQueries.DeleteReservation, parameters);
        }
    }
}
