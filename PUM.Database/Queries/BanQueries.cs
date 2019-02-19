using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUM.Database.Queries
{
    public class BanQueries
    {
        public const string GetBansQuery = @"
SELECT CONCAT(U.FirstName, ' ', U.LastName) AS BannedUsername
    , B.BanID
    , B.UserID
    , B.Reason
    , B.Expire AS ExpirationDate
FROM dbo.Bans B
INNER JOIN dbo.Users U ON B.UserID = U.UserID";

        public const string GetUserBansQuery = @"
SELECT CONCAT(U.FirstName, ' ', U.LastName) AS BannedUsername
    , B.BanID
    , B.UserID
    , B.Reason
    , B.Expire AS ExpirationDate
FROM dbo.Bans B
INNER JOIN dbo.Users U ON B.UserID = U.UserID
WHERE B.UserID = @UserID";

        public const string RemoveBan = @"
DELETE
FROM dbo.Bans
WHERE BanID = @BanID";

    }
}
