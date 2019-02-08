using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace Anonymizer
{
    static class DB
    {
        public static List<Data> ReadData(string dbURL, string dbName, string username, string password)
        {
            var data = new List<Data>();

            var connectionString = new MySqlConnectionStringBuilder();
            connectionString.Server = dbURL;
            connectionString.Database = dbName;
            connectionString.UserID = username;
            connectionString.Password = password;
            
            using(var connection = new MySqlConnection(connectionString.GetConnectionString(true)))
            {
                try
                {
                    connection.Open();
                }
                catch(MySqlException e)
                {
                    Console.WriteLine("Failed to connect to the database.");
                    throw e;
                }
                
                var command = new MySqlCommand(File.ReadAllText("../../../query.sql"), connection);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    data.Add(new Data
                    {
                        AccountName = Utils.Hash(reader.GetString(0)),
                        CharacterName = null,
                        IP = Utils.Hash(reader.GetString(1)),
                        UUID = Utils.Hash(reader.GetString(2)),
                        IPGeolocation = Utils.Hash(reader.GetString(1)), //TODO
                        IsBanned = reader.GetInt32(4) > 0,
                        ActivePlaytime = reader.GetInt32(3)
                    });
                }
            }

            return data;
        }
    }
}
