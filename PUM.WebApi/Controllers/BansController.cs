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
    }
}
