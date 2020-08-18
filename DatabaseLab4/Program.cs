using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DatabaseLab4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string connStr = "server=localhost;User Id=root;database=Shops;CharSet=utf8;password=camaro31";           
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