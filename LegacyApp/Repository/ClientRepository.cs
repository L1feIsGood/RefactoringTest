using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using LegacyApp.Enums;
using LegacyApp.Models;

namespace LegacyApp.Repository
{
    public class ClientRepository : IClientRepository
    {


        public Client GetById(int id)
        {
            const string DatabaseName = "appDatabase";

            Client client = null;
            var connectionString = ConfigurationManager.ConnectionStrings[DatabaseName].ConnectionString;

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
                    var receivedId = int.Parse(reader["ClientId"].ToString());
                    var receivedName = reader["Name"].ToString();
                    var receivedClientStatus = (ClientStatus)int.Parse("ClientStatus");

                    client = new Client
                    {
                        Id = receivedId,
                        Name = receivedName,
                        ClientStatus = receivedClientStatus
                    };
                }
            }

            return client;
        }
    }
}