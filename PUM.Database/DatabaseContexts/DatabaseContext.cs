namespace PUM.Database.DatabaseContexts
{
    using Dapper;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data.SQLite;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Configuration;

    public class DatabaseContext
    {
        public DatabaseContext()
        {
            ConnectionString = WebConfigurationManager.AppSettings["ConnectionString"];
        }

        public List<T> Query<T>(string sqlCommand, Dictionary<string, object> parameters = null)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.Query<T>(sqlCommand, ConvertToDynamicParameters(parameters)).ToList();
            }
        }

        public T QuerySingle<T>(string sqlCommand, Dictionary<string, object> parameters)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.QuerySingle<T>(sqlCommand, ConvertToDynamicParameters(parameters));
            }
        }
        public T QueryFirstOrDefault<T>(string sqlCommand, Dictionary<string, object> parameters)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.QueryFirstOrDefault<T>(sqlCommand, ConvertToDynamicParameters(parameters));
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sqlCommand, Dictionary<string, object> parameters)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return await sqlConnection.QueryFirstOrDefaultAsync<T>(sqlCommand, ConvertToDynamicParameters(parameters));
            }
        }

        protected T ExecuteScalar<T>(string sqlCommand, Dictionary<string, object> parameters)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                return sqlConnection.ExecuteScalar<T>(sqlCommand, ConvertToDynamicParameters(parameters));
            }
        }

        protected void Execute(string sqlCommand, Dictionary<string, object> parameters)
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Execute(sqlCommand, parameters);
            }
        }

        private DynamicParameters ConvertToDynamicParameters(Dictionary<string, object> parameters)
        {
            if (parameters == null)
                return null;

            var dynamicParameters = new DynamicParameters();
            foreach (var param in parameters)
            {
                dynamicParameters.Add(param.Key, param.Value);
            }

            return dynamicParameters;
        }

        protected string ConnectionString { get; set; }
    }
}