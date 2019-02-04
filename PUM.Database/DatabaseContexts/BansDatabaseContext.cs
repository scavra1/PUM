namespace PUM.Database.DatabaseContexts
{
    using PUM.Database.Queries;
    using PUM.SharedModels;
    using System.Collections.Generic;

    public class BansDatabaseContext : DatabaseContext
    {
        public BansDatabaseContext()
        {
        }

        public List<Ban> GetBansList()
        {
            return Query<Ban>(BanQueries.GetBansQuery);
        }
    }
}
