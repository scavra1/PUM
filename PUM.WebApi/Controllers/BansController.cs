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
    }
}
