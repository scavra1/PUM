namespace PUM.WebApi.Controllers
{
    using PUM.Database.DatabaseContexts;
    using PUM.SharedModels;
    using System.Collections.Generic;
    using System.Web.Http;

    public class BansController : ApiController
    {
        private BansDatabaseContext bansContext;
        public BansController()
        {
            bansContext = new BansDatabaseContext();
        }

        public List<Ban> GetBans()
        {
            return bansContext.GetBansList();
        }

        [HttpGet]
        public IHttpActionResult GetUserBans(long userID)
        {
            if (ModelState.IsValid)
            {
                var reservations = bansContext.GetUserBansList(userID);

                return Ok(reservations);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateBan([FromBody] Ban ban)
        {
            bansContext.UpdateBan(ban);

            return Ok("Ban entry has been updated.");
        }

        [HttpPost]
        public IHttpActionResult CreateBan([FromBody] Ban ban)
        {
            bansContext.NewBan(ban);

            return Ok("New ban entry has been added.");
        }

        [HttpDelete]
        public IHttpActionResult RemoveBan([FromUri] long id)
        {
            bansContext.Remove(id);

            return Ok("Ban has been removed.");
        }
    }
}
