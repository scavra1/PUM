namespace PUM.Database.DatabaseContexts
{
    using PUM.Database.Queries;
    using PUM.SharedModels;
    using System.Collections.Generic;

    public class UsersDatabaseContext : DatabaseContext
    {
        public UsersDatabaseContext() : base()
        {
        }

        public User FindUser(UserCredentials credentials)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("Username", credentials.Username);
            parameters.Add("Password", credentials.Password);

            return QueryFirstOrDefault<User>(UserQueries.FindUser, parameters);
        }
    }
}
