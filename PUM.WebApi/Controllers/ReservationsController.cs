namespace PUM.WebApi.Controllers
{
    using PUM.Database.DatabaseContexts;
    using System;
    using System.Web.Http;

    public class ReservationsController : ApiController
    {

        private ReservationsDatabaseContext reservationsContext;

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
    }
}
