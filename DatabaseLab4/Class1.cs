/*

using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;


namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var connectionStringBuilder = new MySqlConnectionStringBuilder();
            connectionStringBuilder.Server = "localhost";
            connectionStringBuilder.UserID = "root";
            connectionStringBuilder.Password = "camaro31";
            connectionStringBuilder.Database = "TEST";
            //connectionStringBuilder.CharacterSet = "utf8";
            
            //var connection = new MySqlConnection($"server=localhost;database=new_schema;uid=root;charSet=utf8");
            var connection = new MySqlConnection(connectionStringBuilder.ConnectionString);
            Console.WriteLine(connectionStringBuilder.ConnectionString);
            //connection.Open();
            try
            {
                connection.Open();
                Console.WriteLine("Yay");

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Bruh");
            }
            //getInfo connection = new getInfo("localhost", 3306, "lab5Database", "root", "camaro31");
            //var listOfSheets = connection.Query<Sheet1>("SELECT * FROM new_schema.`VendorFile - Sheet1`").ToList();
            //Console.WriteLine(listOfSheets[0].C1);
            //Console.WriteLine(listOfSheets[0].C2);
            //Console.ReadLine();
            //getInfo.CreateCommand(, "localhost" );
            
            //var conn = new MySqlConnection($"server={localhost}");


        }
    }
}

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Lab3
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string connStr = "server=localhost;User Id=root;database=;CharSet=utf8;password=camaro31";           
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                Console.WriteLine("Done");
                // Perform database operations
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Bruh");
            }
            conn.Close();

            //var ListOfSheets = conn.Query<Sheet1>
          
            /*string connS = "server=localhost;user=root;database=Lab4;password=Tarakan09;";
            MySqlConnection con = new MySqlConnection(connS);
            conn.Open();
            string sql = "SELECT * FROM Lab4Shops";
            MySqlCommand command = new MySqlCommand(sql, con);
            string name = command.ExecuteScalar().ToString();
            Console.WriteLine(name);
            conn.Close();*/

            
        }
    }
}