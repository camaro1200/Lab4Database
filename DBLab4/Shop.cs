using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace DBLab4
{
    public class Shop
    {
        public int ID { get; set; }
        public string name { get; set; }

        public List<Goods> goodlist = new List<Goods>();

        public int batchprice;
        
        public void displayShopList()
        {
            foreach (Goods x in goodlist)
            {
                Console.WriteLine(x.shop_id + " " + x.ProductName + " " + x.Price + " " + x.Amount);
            }
        }

    }
}