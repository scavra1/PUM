namespace PUM.Database.DatabaseContexts
{
    using PUM.Database.Queries;
    using PUM.SharedModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BansDatabaseContext : DatabaseContext
    {
        public BansDatabaseContext()
        {
        }

        public List<Ban> GetBansList()
        {
            List<Ban> bans = Query<Ban>(BanQueries.GetBansQuery);

            bans.Where(x => x.ExpirationDate >= DateTime.Now).ToList().ForEach(u => u.Active = true);

            return bans;
        }

        public List<Ban> GetUserBansList(long userID)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("UserID", userID);

            List<Ban> bans = Query<Ban>(BanQueries.GetUserBansQuery, parameters);

            bans.Where(x => x.ExpirationDate >= DateTime.Now).ToList().ForEach(u => u.Active = true);

            return bans;
        }

        public void Remove(long id)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("BanID", id);

            Execute(BanQueries.RemoveBan, parameters);
        }
    }
}
