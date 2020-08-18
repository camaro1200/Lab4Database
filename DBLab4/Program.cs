using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;
using System.IO;
using System;

namespace DBLab4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try 
            {
                using (StreamReader sr = new StreamReader("/Users/paulshaburov/Documents/DatabaseLab4/DBLab4/Config.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        
                        if (line == "1")
                        {
                            Console.WriteLine("We read from Config");
                            ConfigNavigator c = new ConfigNavigator();
                            
                            c.CreateShop("Walmart", 1);
                            c.CreateShop("Target", 2);
                            
                            c.makeProduct("Twix");
                            c.makeProduct("Bounty");
                            c.makeProduct("Mars");
                            
                            c.CreateProduct("Twix", 1, 10, 20);
                            c.CreateProduct("Mars", 1, 20, 30);
                            
                            c.CreateProduct("Twix", 2, 5, 25);
                            c.CreateProduct("Mars", 2, 30, 15);
                            
                            c.DisplayShop();

                            c.FindCheapestProduct("Twix");
                            c.FindCheapestProduct("Mars");
                            
                            c.buyGoodsForSum(100, "Walmart");
                            
                            c.getCertainPrice();

                            c.CheapestBatch();

                        }
                
                        else if (line == "0")
                        {
                            Console.WriteLine("we read from Database");
                            Navigation n = new Navigation();
                            n.getconnection();
                            
                            n.CreateShop("Walmart", 1);
                            n.makeProduct("Twix");
                            n.CreateProduct("Twix", 1, 10, 20);
                            n.FindCheapestProduct("Twix");
                            n.buyGoodsForSum(40, "Walmart");
                            n.getCertainPrice();
                            n.CheapestBatch();
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}