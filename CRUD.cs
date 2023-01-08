using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace CRUD_CredentialsSaver
{
    public class CRUD
    {
        static string connStr = "Data Source = CredentialSaver.db";
        public static void Aux()
        {

            using (var connection = new SqliteConnection(connStr))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = 
                    @"CREATE TABLE IF NOT EXISTS dringking_water (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT)";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
