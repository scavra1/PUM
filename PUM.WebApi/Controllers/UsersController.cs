namespace PUM.WebApi.Controllers
{
    using PUM.Database.DatabaseContexts;
    using PUM.SharedModels;
    using System.Web.Http;

    public class UsersController : ApiController
    {
        private UsersDatabaseContext usersContext;

        public UsersController()
        {
            usersContext = new UsersDatabaseContext();
        }

        [HttpPost]
        public User FindUser(UserCredentials credentials)
        {
            return usersContext.FindUser(credentials);
        }
    }
}
