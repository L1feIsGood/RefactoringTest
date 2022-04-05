using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LegacyApp
{
    public class ClientRepository
    {
        public Client GetById(int id)
        {
            try
            {
                Client client = null;
                var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"].ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand
                    {
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "uspGetClientById"
                    };

                    var parameter = new SqlParameter("@ClientId", SqlDbType.Int) { Value = id };
                    command.Parameters.Add(parameter);

                    connection.Open();
                    var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        client = new Client
                        {
                            Id = id,
                            Name = reader["Name"].ToString(),
                            ClientStatus = (ClientStatus)int.Parse("ClientStatus")
                        };
                    }
                }

                return client;
            }
            catch
            {
                return null;
            }
        }
    }
}