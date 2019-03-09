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
                while(reader.Read())
                {
                    data.Add(new Data
                    {
                        AccountName = hashService.Hash(reader.GetString(0)),
                        CharacterName = null,
                        IP = hashService.Hash(reader.GetString(1)),
                        UUID = hashService.Hash(reader.GetString(2)),
                        IPGeolocation = hashService.Hash(ipGeo.GetCountry(reader.GetString(1))),
                        IsBanned = reader.GetInt32(4) > 0,
                        ActivePlaytime = reader.GetInt32(3)
                    });
                }
            }

            return data;
        }

        public static List<Data> GenerateDummyData()
        {
            const int
                ENTRIES = 200000,
                ACCOUNTS = 140000,
                IPS = 160000,
                UUIDS = 150000,
                COUNTRIES = 200,
                BANS = 1500;

            Console.WriteLine("Generating dummy data");

            Console.WriteLine("Pregenerating IP geo maps");
            int[] ipGeoMap = new int[IPS];
            for(int i = 0; i < IPS; i++)
            {
                if(i > 0 && i % 1000 == 0) Console.WriteLine($"{i} / {IPS}");
                ipGeoMap[i] = Math.Min(COUNTRIES - 1, Utils.RandomSkewedDist(10));
            }

            Console.WriteLine("Pregenerating playtime");
            int[] playtime = new int[ACCOUNTS];
            for(int i = 0; i < ACCOUNTS; i++)
            {
                if(i > 0 && i % 1000 == 0) Console.WriteLine($"{i} / {ACCOUNTS}");
                playtime[i] = Utils.RandomSkewedDist(100);
            }

            Console.WriteLine("Pregenerating bans");
            var bans = new HashSet<string>();
            for(int i = 0; i < BANS; i++)
            {
                switch(Utils.rng.Next(3))
                {
                    case 0:
                        bans.Add($"ip{Utils.rng.Next(IPS)}");
                        break;
                    case 1:
                        bans.Add($"uuid{Utils.rng.Next(UUIDS)}");
                        break;
                    case 2:
                        bans.Add($"account{Utils.rng.Next(ACCOUNTS)}");
                        break;
                }
            }

            Console.WriteLine("Generating data entries");
            var data = new List<Data>();
            for(int i = 0; i < ENTRIES; i++)
            {
                if(i > 0 && i % 1000 == 0) Console.WriteLine($"{i} / {ENTRIES}");
                int ip = Utils.rng.Next(IPS);
                int account = Utils.rng.Next(ACCOUNTS);
                int uuid = Utils.rng.Next(UUIDS);

                data.Add(new Data
                {
                    AccountName = account.ToString(),
                    CharacterName = null,
                    IP = ip.ToString(),
                    UUID = uuid.ToString(),
                    IPGeolocation = ipGeoMap[ip].ToString(),
                    IsBanned = bans.Contains($"ip{ip}") || bans.Contains($"account{account}") || bans.Contains($"uuid{uuid}"),
                    ActivePlaytime = playtime[account]
                });
            }

            Console.WriteLine("Finished generating data");

            return data;
        }
    }
}
