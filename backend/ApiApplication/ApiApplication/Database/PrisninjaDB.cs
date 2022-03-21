using System.Data.SqlClient;

namespace ApiApplication.Database;

public class PrisninjaDB
{
    private string _connectionString = "Data Source=localhost;" +
                                       "Database=master;" +
                                       "TrustServerCertificate=true;" +
                                       "User ID=SA;" +
                                       "PASSWORD=<Tofirebananer147>";
    
    public PrisninjaDB()
    {
        RunQueryAsync(File.ReadAllText("Database/CreatePrisninjaDB.sql"));
    }

    private async Task<string> RunQueryAsync(string query)
    {
        string result = new string("");
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            await connection.OpenAsync();
            await using (SqlDataReader reader = command.ExecuteReaderAsync().Result)
            {
                while (reader.ReadAsync().Result)
                {
                    result = result.Insert(result.Length, reader[0].ToString());
                }
            }
        }
        return result;
    }
}