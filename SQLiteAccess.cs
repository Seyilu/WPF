using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Data;

namespace WpfApp
{
    class SQLiteAccess
    {
        public static string dbFileDirectory = Environment.CurrentDirectory;
        private static string dbFilePath = System.IO.Path.Combine(dbFileDirectory, "database.db");
        private static string _connectionString = string.Format("Data Source = {0}", dbFilePath);


        // Read
        public static List<ZwierzetaModel> Read()
        {
            List<ZwierzetaModel> ZwierzetaList = new List<ZwierzetaModel>();
            SqliteConnection dbConnection = new SqliteConnection(_connectionString);

            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = "SELECT * FROM Zwierzeta";
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);

                SqliteDataReader dbDataReader = dbCommand.ExecuteReader();

                while (dbDataReader.Read())
                {
                    ZwierzetaModel Zwierzeta = new ZwierzetaModel()
                    {
                        Id = Convert.ToInt32(dbDataReader["Id"]),
                        Name = dbDataReader["Name"].ToString(),
                        Species = dbDataReader["Species"].ToString(),
                        Description = dbDataReader["Description"].ToString(),
                        PhotoName = dbDataReader["PhotoName"].ToString()
                    };
                    ZwierzetaList.Add(Zwierzeta);
                }
            }
            dbConnection.Close();
            return ZwierzetaList;
        }

        public static bool Create(ZwierzetaModel item)
        {
            bool isCreated = false;
            SqliteConnection dbConnection = new SqliteConnection(_connectionString);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = string.Format("SELECT COUNT(Id) FROM Zwierzeta WHERE Name = '{0}'", item.Name);
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);

                long result = (long)dbCommand.ExecuteScalar();
                if (result == 0)
                {
                    dbQuery = string.Format("INSERT INTO Zwierzeta VALUES(null, '{0}', '{1}', '{2}', '{3}' )", item.Name, item.Species, item.Description, item.PhotoName);
                    dbCommand.CommandText = dbQuery;
                    if (dbCommand.ExecuteNonQuery() == 1)
                    {
                        isCreated = true;
                    }
                }
            }
            dbConnection.Close();
            return isCreated;
        }

        public static bool Delete(ZwierzetaModel item)
        {
            bool isDeleted = false;
            SqliteConnection dbConnection = new SqliteConnection(_connectionString);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = string.Format("SELECT COUNT(Id) FROM Zwierzeta WHERE Name = '{0}'", item.Name);
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);

                long result = (long)dbCommand.ExecuteScalar();
                if (result == 1)
                {
                    dbQuery = string.Format("DELETE FROM Zwierzeta WHERE Name = '{0}'", item.Name);
                    dbCommand.CommandText = dbQuery;

                    if (dbCommand.ExecuteNonQuery() == 1)
                    {
                        isDeleted = true;
                    }
                }
            }
            dbConnection.Close();
            return isDeleted;
        }

        public static bool Update(ZwierzetaModel item)
        {
            bool isUpdated = false;
            SqliteConnection dbConnection = new SqliteConnection(_connectionString);
            dbConnection.Open();
            if (dbConnection.State == ConnectionState.Open)
            {
                string dbQuery = string.Format("SELECT COUNT(Id) FROM Zwierzeta WHERE Name = '{0}'", item.Name);
                SqliteCommand dbCommand = new SqliteCommand(dbQuery, dbConnection);

                long result = (long)dbCommand.ExecuteScalar();
                if (result == 1)
                {
                    dbQuery = string.Format("UPDATE Zwierzeta SET Name = '{0}', Species = '{1}', Description = '{2}', PhotoName = '{3}' WHERE Name = '{0}'", item.Name, item.Species, item.Description, item.PhotoName);
                    dbCommand.CommandText = dbQuery;

                    dbCommand.ExecuteNonQuery();
                    isUpdated = true;
                }
            }
            dbConnection.Close();
            return isUpdated;
        }
    }
}
