using System;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;

namespace DBLab4
{
    public class Navigation : ShopService
    {
        public string connStr = "server=127.0.0.1;User Id=root;database=Shops;CharSet=utf8;password=camaro31";
        public MySqlConnection conn;
        
        public List<Shop> shopList = new List<Shop>();
        public List<string>allGoods = new List<string>();
        
        public void getconnection()
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                Console.WriteLine("Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Bruh");
            }
            conn.Close();
        }
        
        public void CreateShop(string ShopName, int ID)
        {
            string query = $"INSERT INTO Shops.Vendors (ID, name) VALUES ({ID}, '{ShopName}')";
            MySqlCommand command = new MySqlCommand(query, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            
            Shop s1 = new Shop();
            s1.name = ShopName;
            s1.ID = ID;
            shopList.Add(s1);
        }

        public void makeProduct(string name)
        {
            for (int i = 0; i < allGoods.Count; i++)
            {
                if (allGoods[i] == name)
                    return;
            }
            
            allGoods.Add(name);
        }

        
        public void CreateProduct(string ProductName, int ID,  int Amount, int Price)
        {
            makeProduct(ProductName);
            
            string query = $"INSERT INTO Shops.Products (shop_id, ProductName , Amount, Price) VALUES ({ID}, '{ProductName}', {Amount}, {Price})";
            MySqlCommand command = new MySqlCommand(query, conn);
            conn.Open();
            command.ExecuteNonQuery();
            conn.Close();
            
            Goods g1 = new Goods();
            g1.ProductName =  ProductName;
            g1.Price = Price;
            g1.Amount = Amount;
            g1.shop_id = ID;
            
            Shop temp = null;
            for (int j = 0; j < shopList.Count; j++)
            {
                temp = shopList[j];
                if (temp.ID == g1.shop_id)
                {
                    shopList[j].goodlist.Add(g1);
                }
            }
        }

        public void DisplayShop()
        {
            foreach (Shop x in shopList)
            {
                foreach (Goods i in x.goodlist)
                {
                    Console.WriteLine(i.shop_id + " " + i.ProductName + " " + i.Price + " " + i.Amount);
                }
            }
        }
        
        public Shop FindCheapestProduct(string name)
        {
            Shop s = shopList[0];
            int cheap = 99999;
            foreach (Shop x in shopList)
            {
                foreach (Goods g in x.goodlist)
                {
                    if (g.ProductName == name)
                    {
                        if (g.Price < cheap)
                        {
                            s = x;
                            cheap = g.Price;
                        }
                    }
                }
            }
            Console.WriteLine(s.name);
            return s;
        }
        
        public void buyGoodsForSum(int sum, string shop_name)
        {
            int temp = sum;
            int cnt = 0;
            foreach (Shop x in shopList)
            {
                if (x.name == shop_name)
                {
                    foreach (Goods i in x.goodlist)
                    {
                        for (int j = 0; j < i.Amount; j++)
                        {
                            if (temp >= i.Price)
                            {
                                temp -= i.Price;
                                cnt++;
                            }
                        }
                        Console.WriteLine(i.ProductName + " " + cnt + " " + temp);
                        cnt = 0;
                        temp = sum;
                    }
                }
            }
        }
        
        public void getCertainPrice()
        {
            int sum = 0;
            int flag = 0;
            
            Console.WriteLine("What shop");
            string key = Console.ReadLine();
            foreach (Shop x in shopList)
            {
                if (x.name == key)
                {
                    flag = 1;
                    Console.WriteLine("how many Goods");
                    int n = Convert.ToInt32(Console.ReadLine());
                    
                    if (x.goodlist.Count < n)
                    {
                        Console.WriteLine("fail");
                        return;
                    }

                    for (int i = 0; i < n; i++)
                    {
                        int checkflag = 0;
                        Console.WriteLine("What good");
                        string searchGood = Console.ReadLine();
                        Console.WriteLine("Amount?");
                        int searchAmount = Convert.ToInt32(Console.ReadLine());
                        
                        foreach (Goods g in x.goodlist)
                        {
                            if (searchGood == g.ProductName && searchAmount <= g.Amount)
                            {
                                checkflag = 1;
                                sum += g.Price * searchAmount;
                            }
                        }

                        if (checkflag == 0)
                        {
                            Console.WriteLine("failed");
                            return;
                        }
                    }
                }
            }
            if (flag == 0)
                Console.WriteLine("no such shop exists");
            
            Console.WriteLine(sum);
        }

        public void countBatch(string name, int amount)
        {
            foreach (Shop x in shopList)
            {
                if(x.batchprice == -1)
                    return;
                
                int flag = 0;
                foreach (Goods g in x.goodlist)
                {
                    if (g.ProductName == name && g.Amount >= amount)
                    {
                        x.batchprice += amount * g.Price;
                        flag = 1;
                    }
                }

                if (flag == 0)
                    x.batchprice = -1;
            }
        }
        
        public Shop CheapestBatch()
        {
            Console.WriteLine("how many Goods");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string name = Console.ReadLine();
                int amounnt = Convert.ToInt32(Console.ReadLine());
                countBatch(name, amounnt);
            }

            Shop s = new Shop();
            s.batchprice = 99999;
            foreach (Shop x in shopList)
            {
                if (x.batchprice < s.batchprice && x.batchprice != -1)
                {
                    s.batchprice = x.batchprice;
                    s = x;
                }
            }

            foreach (Shop x in shopList)
            {
                x.batchprice = 0;
            }
            foreach (Shop x in shopList)
            {
                x.batchprice = 0;
            }
            
            Console.WriteLine(s.name);
            return s;
        }
        
        //Extra funnction, not really needed!
        // takes everything already in Database and adds it to list
        public void getShops()
        {
            var list = conn.Query<Shop>("SELECT * FROM Shops.Vendors").ToList();
            int cnt = list.Count;
            for (int i = 0; i < cnt; i++)
            {
                Shop s1 = new Shop();
                s1.name = list[i].name;
                s1.ID = list[i].ID;
                shopList.Add(s1);
         
            }
        }

        //Extra funnction, not really needed!
        //Gets everything already in Database, used at start
        public void getProduct()
        {
            var list = conn.Query<Goods>("SELECT * FROM Shops.Products").ToList();
            for (int i = 0; i < list.Count; i++)
            {
                Goods g1 = new Goods();
                g1.ProductName = list[i].ProductName;
                g1.Price = list[i].Price;
                g1.Amount = list[i].Amount;
                g1.shop_id = list[i].shop_id;
                allGoods.Add(list[i].ProductName);

                Shop temp = null;
                for (int j = 0; j < shopList.Count; j++)
                {
                    temp = shopList[j];
                    if (temp.ID == g1.shop_id)
                    {
                        shopList[j].goodlist.Add(g1);
                    }
                }
            }
        }
    }
}