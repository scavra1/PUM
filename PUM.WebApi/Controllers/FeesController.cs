using PUM.Database.DatabaseContexts;
using PUM.SharedModels;
using System.Collections.Generic;
using System.Web.Http;

namespace PUM.WebApi.Controllers
{
    public class FeesController : ApiController
    {
        private FeesDatabaseContext feesContext;
        public FeesController()
        {
            feesContext = new FeesDatabaseContext();
        }

        public List<Fee> GetFees()
        {
            return feesContext.GetFeesList();
        }

        [HttpGet]
        public IHttpActionResult GetUserFees(long userID)
        {
            if (ModelState.IsValid)
            {
                var reservations = feesContext.GetUserFeesList(userID);

                return Ok(reservations);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}