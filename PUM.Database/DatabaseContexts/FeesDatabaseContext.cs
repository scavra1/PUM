using PUM.Database.Queries;
using PUM.SharedModels;
using System.Collections.Generic;

namespace PUM.Database.DatabaseContexts
{
    public class FeesDatabaseContext : DatabaseContext
    {
        public FeesDatabaseContext()
        {
        }

        public List<Fee> GetFeesList()
        {
            return Query<Fee>(FeeQueries.GetFeesQuery);
        }

        public List<Fee> GetUserFeesList(long userID)
        {
            var parameters = new Dictionary<string, object>();

            parameters.Add("UserID", userID);

            return Query<Fee>(FeeQueries.GetUserFeesQuery, parameters); ;
        }
    }
}
