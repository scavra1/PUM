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
    }
}
