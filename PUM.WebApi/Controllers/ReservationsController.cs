namespace PUM.WebApi.Controllers
{
    using PUM.Database.DatabaseContexts;
    using System;
    using System.Web.Http;

    public class ReservationsController : ApiController
    {

        private ReservationsDatabaseContext reservationsContext;

        public ReservationsController()
        {
            reservationsContext = new ReservationsDatabaseContext();
        }

        [HttpPost]
        public IHttpActionResult Reserve([FromBody] long userId, [FromBody] DateTime dateTime)
        {
            if (ModelState.IsValid == true)
            {
                reservationsContext.ReserveForDay(userId, dateTime);

                return Ok();
            }
            else
            {
                return BadRequest("Invalid request model");
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
    }
}
