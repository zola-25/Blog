using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.Services
{
    public class DatabaseCreator : IDatabaseCreator
    {
        public void CreateDatabase(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            string dbToCreate = builder.InitialCatalog;

            string masterDbConnectionString = connectionString.Replace(dbToCreate, "master"); // Ideally would use the SqlConnectionStringBuilder to just replace the db name with master, but when the ConnectionString property is called on the builder it always returns a connection string with a DataSource=... property instead of Server=..., which for some reason doesn't work on Azure

            using (var conn = new SqlConnection(masterDbConnectionString))
            {
                conn.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = conn;
                    CheckDbName(dbToCreate); // You can't use parameters when using SQL DDL like CREATE DATABASE, so have to validate SQL manually
                    command.CommandTimeout = 60;
                    command.CommandText = $@" 

                    IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{dbToCreate}')
                    BEGIN
                        CREATE DATABASE [{dbToCreate}] (
                        MAXSIZE=2 GB,
                        EDITION='Standard',
                        SERVICE_OBJECTIVE='S0') 
                    END";

                    command.ExecuteNonQuery();
                }
            }
        }

        private void CheckDbName(string dbName)
        {
            if (dbName.Any(c => !(Char.IsLetterOrDigit(c) || c == '-')))
            {
                throw new Exception("DB name is invalid");
            }
        }
    }
}
