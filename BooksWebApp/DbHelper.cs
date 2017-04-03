using System.Data;
using System.Data.SqlClient;

namespace BooksWebApp
{
    public class DbHelper
    {
        private static string ConnectionString
        {
            get
            {
                return System.Configuration
                    .ConfigurationManager
                    .ConnectionStrings["BooksSqlServerConn"].ConnectionString;
            }
        }

        public DataTable GetResultSet(string sql)
        {
            DataTable dt = new DataTable();
            using(SqlDataAdapter da = new SqlDataAdapter(sql, ConnectionString))
            {
                da.Fill(dt);
            }
            return dt;
        }

        public void SqlExecute(string sql)
        {
            using(SqlCommand cmd = new SqlCommand(sql, new SqlConnection(ConnectionString)))
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
    }
}