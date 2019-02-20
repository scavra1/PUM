namespace PUM.WebApi.Controllers
{
    using PUM.Database.DatabaseContexts;
    using PUM.SharedModels;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class ReservationsController : ApiController
    {

        private ReservationsDatabaseContext reservationsContext;
        private BansDatabaseContext bansContext;

        public ReservationsController()
        {
            reservationsContext = new ReservationsDatabaseContext();
            bansContext = new BansDatabaseContext();
        }

        [HttpPost]
        public HttpResponseMessage AddReservation([FromBody] Reservation reservation)
        {
            var bans = bansContext.GetUserBansList(reservation.UserID.Value);
            var activeBan = bans.FirstOrDefault(b => b.ExpirationDate > DateTime.Now);

            if (activeBan != null)
            {
                var message = @"You are banned.";
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }

            var canUserReserveForDay = reservationsContext.CheckDailyReservationForUser(reservation.UserID.Value, reservation.DateKey);

            if (canUserReserveForDay == true)
            {
                var reservationDateWeekDay = reservation.Date.DayOfWeek;

                var startDate = reservation.Date.AddDays(1 - (int)reservationDateWeekDay);
                var startDateKey = int.Parse(startDate.ToString("yyyyMMdd"));
                var endDateKey = startDateKey + 5;

                var canUserReserveForWeek = reservationsContext.CheckWeeklyReservation(reservation.UserID, startDateKey, endDateKey);
                if (canUserReserveForWeek == true)
                {
                    reservationsContext.ReserveDateForUser(reservation.UserID.Value, reservation.DateKey, reservation.HourKey);

                    var message = @"You have reserved chosen date.";
                    return Request.CreateResponse(HttpStatusCode.OK, message);
                }
                else
                {
                    var message = @"You have already reserved for given week.";
                    return Request.CreateResponse(HttpStatusCode.Forbidden, message);
                }
            }
            else
            {
                var message = @"You have already reserved for given day.";
                return Request.CreateResponse(HttpStatusCode.Forbidden, message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetReservations(int dateKey)
        {
            if (ModelState.IsValid)
            {
                var reservations = reservationsContext.GetReservations(dateKey);

                return Ok(reservations);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public IHttpActionResult GetUserReservations(long userID)
        {
            if (ModelState.IsValid)
            {
                var reservations = reservationsContext.GetUserReservations(userID);

                return Ok(reservations);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteReservation([FromUri] int reservationId)
        {

            reservationsContext.DeleteReservation(reservationId);

            return Ok();
        }
    }
}
