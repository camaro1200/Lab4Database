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
    public class ConfigNavigator : ShopService
    {
        public List<Shop> shopList = new List<Shop>();
        public List<string>allGoods = new List<string>();
        
        // creates shop
        public void CreateShop(string shop_name, int ID)
        {
            Shop s1 = new Shop();
            s1.name = shop_name;
            s1.ID = ID;

            foreach (Shop x in shopList)
            {
                if (x.ID == ID)
                {
                    return;
                }
                    
            }
            shopList.Add(s1);
            
            using (StreamWriter sw = File.AppendText("/Users/paulshaburov/Documents/DatabaseLab4/DBLab4/Shops.txt"))
            {
                sw.WriteLine(shop_name + "," + ID);
            }	
        }
        // creates product and assigns it to store
        public void CreateProduct(string name, int id, int amount, int price)
        {
            using (StreamWriter sw = File.AppendText("/Users/paulshaburov/Documents/DatabaseLab4/DBLab4/Goods.txt"))
            {
                sw.WriteLine(name + "," + id + "," + amount + "," + price);
            }	
            
            Goods g1 = new Goods();
            g1.ProductName = name;
            g1.Price = price;
            g1.Amount = amount;
            g1.shop_id = id;

            makeProduct(g1.ProductName);

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
        //Создать товар
        public void makeProduct(string name)
        {
            for (int i = 0; i < allGoods.Count; i++)
            {
                if (allGoods[i] == name)
                    return;
            }
            
            allGoods.Add(name);
        }
        
        // displays all products that are inside a shop
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
        
        //find shop with cheapest given product
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
        
        //given sum and shop see what goods u can buy for that sum
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
                        Console.WriteLine("product: " + i.ProductName + " amount: " + cnt + " change: " + temp);
                        cnt = 0;
                        temp = sum;
                    }
                }
            }
        }
        
        // Купить партию товаров в магазине (параметры - сколько каких товаров купить,
        // метод возвращает общую стоимость покупки либо её невозможность, если товара
        // не хватает)

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
            
            Console.WriteLine(sum);
        }
        
        // find sum of certain batch in each store
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
        
        // Найти, в каком магазине партия товаров (набор товар-количество) имеет
        // наименьшую сумму (в целом). Например, «в каком магазине дешевле всего купить
        // 10 гвоздей и 20 шурупов». Наличие товара в магазинах учитывается!
        
        public Shop CheapestBatch()
        {
            Console.WriteLine("how many Goods");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("name of good");
                string name = Console.ReadLine();
                Console.WriteLine("amount");
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

    }
}