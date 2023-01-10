using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace CRUD_CredentialsSaver
{
    public class CRUD
    {
        static string connStr = "Data Source = CredentialSaver.db";
        static string GetStringInput(string param)
        {
            Console.Write($"{param}! ");
            string value = Convert.ToString(Console.ReadLine());
            return value;
        }
        static int GetIntInput(string param)
        {
            Console.Write($"{param}: "); string readed = Console.ReadLine();
            int aux;
            if (!int.TryParse(readed, out aux))
                Console.Write($"Error: {param} no valid! try again: "); readed = Console.ReadLine();
            
            return aux;
        }
        public static void Insert()
        {   
            string mail = GetStringInput("Email!");
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

                foreach(var item in records)
                {
                    Console.WriteLine(String.Format("Id: {0}",item.Id));
                    Console.WriteLine(String.Format("Email: {0}",item.Email));
                    Console.WriteLine(String.Format("NickName: {0},{1}",item.NickName, "\n"));
                }
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
                    Console.WriteLine($"'{rowCount}' row(s) deleted");
                }
                else
                {
                    Console.WriteLine("That row does not exist!");
                }

                connection.Close();
            }
        }
        public static void Update()
        {
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
                    Console.WriteLine($"There is no Record for this Id: {Id}");
                    //connection.Close();
                    Update();
                }
                string email = GetStringInput("Email!");
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
