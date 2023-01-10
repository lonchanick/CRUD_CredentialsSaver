using System.Collections.Generic;
using ConsoleTables;
using Microsoft.Data.Sqlite;

namespace CRUD_CredentialsSaver
{
    public class CRUD
    {
        static void PrettyTable(List<Credential> records)
        {
            var table = new ConsoleTable("Id", "Email", "Nickname", "Fecha");
            foreach (var item in records)
                table.AddRow(item.Id, item.Email, item.NickName, "Warever");
            table.Write(Format.MarkDown);
        }
        static string connStr = "Data Source = CredentialSaver.db";
        static string GetStringInput(string param)
        {
            Console.Write($"{param}: ");
            string value = Console.ReadLine();

            while (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("Empty values are not allowed, Try again");
                value = Console.ReadLine();

            }

            return value;
        }
        static int GetIntInput(string param)
        {
            Console.Write($"{param}: "); string readed = Console.ReadLine();
            int aux;
            if (!int.TryParse(readed, out aux))
            {
                Console.Write($"Error: {param} is not a valid input! try again: ");
                readed = Console.ReadLine();
            }
            
            return aux;
        }
        public static void Insert()
        {   
            string mail = GetStringInput("Email");
            string nickname = GetStringInput("Nickname");
            
            using (var connection = new SqliteConnection(connStr))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText =
                    $"INSERT INTO CredentialSaver(Email, NickName) VALUES('{mail}','{nickname}')";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public static void GetAllRecords()
        {
            using (var connection = new SqliteConnection(connStr))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"SELECT * FROM CredentialSaver";
                List<Credential> records = new ();
                SqliteDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows) 
                {
                    while(reader.Read())
                    {
                        records.Add(
                            new Credential() 
                            {
                                Id = reader.GetInt32(0),
                                Email = reader.GetString(1).ToString(),
                                NickName = reader.GetString(2).ToString()
                            }
                            );
                    }
                }
                else
                {
                    Console.WriteLine("There are no records yet");
                }
                connection.Close();
                PrettyTable(records);
            }
        }
        public static void Delete()
        {
            int Id = GetIntInput("Id");
            using (var connection = new SqliteConnection(connStr))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = $"DELETE FROM CredentialSaver WHERE Id = '{Id}'";
                var rowCount = cmd.ExecuteNonQuery();
                if(rowCount > 0)
                {
                    Console.WriteLine($"'{rowCount}' row deleted");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Row does not exist!");
                    Console.ReadLine();
                }

                connection.Close();
            }
        }
        public static void Update()
        {
            Console.WriteLine("Seleccione un Id");
            int Id = GetIntInput("Id");
            using (var connection = new SqliteConnection(connStr))
            {
                //Console.Write("Type an Id: ");  int Id = Convert.ToInt32(Console.ReadLine());
                connection.Open();
                var cmd2 = connection.CreateCommand();
                cmd2.CommandText = $"SELECT EXISTS(SELECT 1 FROM CredentialSaver WHERE Id = '{Id}')";
                int checkQuery = Convert.ToInt32(cmd2.ExecuteScalar());
                if (checkQuery == 0)
                {
                    Console.WriteLine($"Id '{Id}' does not Exist");
                    //connection.Close();
                    Update();
                }
                Console.WriteLine($"Updating Id: {Id}");
                string email = GetStringInput("Email");
                string nickname = GetStringInput("Nickname");
                var cmd = connection.CreateCommand();
                cmd.CommandText = 
                    $"UPDATE CredentialSaver SET Email = '{email}', NickName = '{nickname}' " +
                    $"WHERE Id = '{Id}'";
                var rowCount = cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
