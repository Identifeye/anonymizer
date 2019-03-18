using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Anonymizer
{
    static class DB
    {
        public static List<Data> ReadData(string dbURL, string dbName, string username, string password, int coresToUse)
        {
            string secretSalt = BCrypt.Net.BCrypt.GenerateSalt();
            Console.WriteLine($"Saving data using the salt {secretSalt}");
            Console.WriteLine("Please save this salt if you ever want to parse the results of Identifeye.");

            var data = new List<Data>();
            var ipGeo = new IPGeolocationService();
            var hashService = new HashService(secretSalt);

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
                
                var command = new MySqlCommand(File.ReadAllText("query.sql"), connection);
                var reader = command.ExecuteReader();

                Console.WriteLine("Reading entries from database...");
                int counter = 0;

                while(reader.Read())
                {
                    counter++;
                    if(counter % 500 == 0)
                    {
                        Console.WriteLine(counter);
                    }

                    data.Add(new Data
                    {
                        AccountName = reader.GetString(0),
                        CharacterName = null,
                        IP = reader.IsDBNull(1) ? null : reader.GetString(1),
                        UUID = reader.IsDBNull(2) ? null : reader.GetString(2),
                        IPGeolocation = reader.IsDBNull(1) ? null : ipGeo.GetCountry(reader.GetString(1)),
                        IsBanned = reader.GetInt32(4) > 0,
                        ActivePlaytime = reader.GetInt32(3)
                    });
                }
            }

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = coresToUse
            };

            Console.WriteLine("Finished reading data from database. Encrypting data...");

            int count = 0;
            object countLock = new object();
            
            // Using BCrypt on hundreds of thousands of entries is very slow, so we're utilizing all the cores of the machine to make it faster
            Parallel.ForEach(data, options, d =>
            {
                lock(countLock)
                {
                    count++;
                    if(count % 50 == 0)
                    {
                        Console.WriteLine($"{count} / {data.Count}");
                    }
                }
                d.AccountName = hashService.Hash(d.AccountName);
                d.IP = hashService.Hash(d.IP);
                d.UUID = hashService.Hash(d.UUID);
                d.IPGeolocation = hashService.Hash(d.IPGeolocation);
            });

            return data;
        }
    }
}
