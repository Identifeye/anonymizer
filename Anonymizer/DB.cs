using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

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

                string query =
                    "SELECT users.Username, knownips.IP, users.UUID, bans.banID, bans.Expiry, bans.UnbanDate, users.ActivePlaytime FROM users " +
                    "LEFT JOIN knownips ON users.Username = knownips.Username " +
                    "LEFT JOIN bans ON users.Username = bans.Username";

                var command = new MySqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    bool banned = false;
                    if(!reader.IsDBNull(3)) // This block is called if the BanId value isn't null, meaning that this user has some ban associated with them.
                    {
                        //TODO: this won't work. You'll just get every combination of associated IP and ban for a user, each in their own row.
                        long banExpiry = reader.IsDBNull(4) ? long.MaxValue : reader.GetInt64(4);
                        long unbanDate = reader.IsDBNull(5) ? long.MaxValue : reader.GetInt64(5);
                        banned = DateTime.UtcNow.ToFileTimeUtc() < Math.Min(banExpiry, unbanDate);
                    }

                    data.Add(new Data
                    {
                        AccountName = Utils.Hash(reader.GetString(0)),
                        CharacterName = null,
                        IP = Utils.Hash(reader.GetString(1)),
                        UUID = Utils.Hash(reader.GetString(2)),
                        IPGeolocation = Utils.Hash(reader.GetString(1)), //TODO
                        IsBanned = banned,
                        ActivePlaytime = reader.GetInt32(6)
                    });
                }
            }

            return data;
        }
    }
}
